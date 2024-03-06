using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.PlayerSettings;

public class PlayerStateManager : MonoBehaviour
{
    public static string currentState = "idle";

    public static bool canAttack = false;

    public float shootCooldown = 1;

    public float chompAttackCooldown;

    public GameObject acidPoint;
    public GameObject acidRadiusShape;
    public GameObject acidPrefab;
    public GameObject acidEndPoint;
    public GameObject acidPuddlePrefab;

    public GameObject projectiles;

    public GameObject chompPoint;
    public GameObject LungePoint;

    public GameObject playerSprite;

    public Sprite[] possibleAcidSprites;
    Vector3 acidPuddleSpawnPoint;
    GameObject acidPuddle;
    public Rigidbody2D Rigidbody2D;

    float lastChomp;

    public float LungeCharge = 0;
    public bool IsLungeCharging = false;

    Vector3 LungeDir;

    private void Update()
    {
        ManageAcid();
        ManageLunge();
    }

    public void Chomp()
    {
        if (currentState == "vomitting")
        {
            return;
        }

        if (Time.time > lastChomp + chompAttackCooldown)
        {
            // anim goes here
            //playerSprite.GetComponent<Animator>().Play("chomp", -1, 0f);

            HitPoint(chompPoint.transform.position, 1.2f, 10);

            lastChomp = Time.time;
        }
    }

    public void HitPoint(Vector2 pos, float radius, float damage)
    {
        Collider2D hit = Physics2D.OverlapCircle(chompPoint.transform.position, radius);

        Debug.Log("hit something");

        if (hit.CompareTag("Enemy"))
        {
            hit.GetComponentInParent<Dummy>().TakeDamage(damage);
            Debug.Log("hit enemy");
        }
    }

    public void AcidAttack()
    {
        acidPuddle = null;
        acidPuddleSpawnPoint = acidEndPoint.transform.position;

        for (int i = 0; i < Random.Range(7, 15); i++)
        {
            int spriteIndex = Random.Range(1, 3);

            Vector3 randomPos = Random.insideUnitCircle * 1;
            
            GameObject newAcid = Instantiate(acidPrefab, projectiles.transform);
            newAcid.GetComponent<AcidController>().stateManager = GetComponent<PlayerStateManager>();
            newAcid.transform.position = acidPoint.transform.position;
            newAcid.GetComponent<AcidController>().direction = acidEndPoint.transform.position + new Vector3(randomPos.x, randomPos.y, 0);
            newAcid.GetComponent<SpriteRenderer>().sprite = possibleAcidSprites[spriteIndex];
        }
    }

    void ManageLunge()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            LungeCharge += Time.deltaTime * 7.5f;
            PlayerMovement.moveSpeed = 0;
        }
        if (IsLungeCharging && LungeCharge >= 15)
        {
            LungeCharge = 20;
        }
        else if (!IsLungeCharging)
        {
            LungeCharge = 0;
        }

    }

    public void LungeAttack()
    {
        HitPoint(LungePoint.transform.position, 4, 10);
        Rigidbody2D.velocity = transform.right * LungeCharge;
    }

    void ManageAcid()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        acidPoint.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        acidRadiusShape.SetActive(currentState == "vomitting");
    }

    public void SpawnAcidPuddle()
    {
        if (!acidPuddle)
        {
            GameObject newAcidPuddle = Instantiate(acidPuddlePrefab, projectiles.transform);
            newAcidPuddle.transform.position = acidPuddleSpawnPoint;

            acidPuddle = newAcidPuddle;
        }
    }
}
