using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public class PlayerStateManager : MonoBehaviour
{
    public static string currentState = "idle";

    public static bool canAttack = false;

    private int lives = 3;

    public float shootCooldown = 1;
    public float LungeTrailTimer = 0;
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

    public LayerMask wallMask;

    public Sprite[] possibleAcidPuddleSprites;
    public Sprite[] possibleAcidSprites;

    Vector3 acidPuddleSpawnPoint;
    GameObject acidPuddle;
    public Rigidbody2D Rigidbody2D;

    float lastChomp;

    public float LungeCharge = 0;
    public bool IsLungeCharging = false;
    public bool IsLunging = false;

    Vector3 LungeDir;

    private void Update()
    {
        ManageAcid();
        ManageLunge();
        if (IsLunging == true) 
        {
            AcidLunge();
        }
    }

    public void TakeLife(int livesTaken)
    {
        lives -= livesTaken;
        Debug.Log(lives);
        if (lives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Chomp()
    {
        if (currentState == "vomitting")
        {
            return;
        }

        if (Time.time > lastChomp + chompAttackCooldown)
        {
                
            HitPoint(chompPoint.transform.position, 1.2f, 1);

            lastChomp = Time.time;
        }
    }

    public void HitPoint(Vector2 pos, float radius, float damage)
    {
        Collider2D hit = Physics2D.OverlapCircle(chompPoint.transform.position, radius);
        if (hit != null)
        {
            if (!hit.CompareTag("Player"))
            {
                Debug.Log("hit something");
            }

            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponentInParent<HealthComponent>().TakeDamage(damage);
                Debug.Log("hit enemy");
                if (IsLunging)
                {
                    Debug.Log("hit enemy while lunging");
                }
            }

            DoorComponent doorComp = hit.GetComponent<DoorComponent>();

            if (hit.CompareTag("Door") && doorComp)
            {
                if (doorComp.doorLevel > 1)
                    return;

                doorComp.HitDoor();
            }
        }
    }

    public void AcidAttack()
    {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        RaycastHit2D result = Physics2D.Raycast(transform.position, mouseWorldPos, 100f, wallMask.value);

        acidPuddle = null;

        for (int i = 0; i < Random.Range(7, 15); i++)
        {
            int spriteIndex = Random.Range(1, 3);
            
            Vector3 randomPos = Random.insideUnitCircle * 1;
            Debug.Log(possibleAcidSprites[spriteIndex]);
            GameObject newAcid = Instantiate(acidPrefab, projectiles.transform);
            newAcid.GetComponent<AcidController>().stateManager = GetComponent<PlayerStateManager>();
            newAcid.transform.position = acidPoint.transform.position;
            newAcid.GetComponent<AcidController>().direction = acidEndPoint.transform.position + new Vector3(randomPos.x, randomPos.y, 0);
            newAcid.GetComponent<SpriteRenderer>().sprite = possibleAcidSprites[spriteIndex];
        }
        
        if (result)
        {
            SpawnAcidPuddle(result.point);
        }
        else
        {
            SpawnAcidPuddle(acidEndPoint.transform.position);
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
        IsLunging = true;
        Rigidbody2D.velocity = transform.right * LungeCharge;
    }

    public void AcidLunge()
    {
        if (IsLunging == true)
        {
            LungeTrailTimer += Time.deltaTime;
            if (LungeTrailTimer >= 0.3f)
            {
             SpawnAcidPuddle(acidPoint.transform.position);
             HitPoint(LungePoint.transform.position, 4, 20);
                LungeTrailTimer = 0f;
            }
        }
    }

    void ManageAcid()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        acidPoint.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        acidRadiusShape.SetActive(currentState == "vomitting");
    }

    public void SpawnAcidPuddle(Vector3 location)
    {
        if (!acidPuddle)
        {
            int spriteIndex = Random.Range(1, 3);

            GameObject newAcidPuddle = Instantiate(acidPuddlePrefab, projectiles.transform);
            newAcidPuddle.transform.position = location;
            newAcidPuddle.GetComponent<SpriteRenderer>().sprite = possibleAcidPuddleSprites[spriteIndex];

            acidPuddle = newAcidPuddle;
        }
    }
}
