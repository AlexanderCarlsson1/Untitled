using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerStateManager : MonoBehaviour
{
    public static string currentState = "idle";

    public static bool canAttack = false;

    public GameObject acidPoint;
    public GameObject acidRadiusShape;
    public GameObject acidPrefab;
    public GameObject acidEndPoint;

    public GameObject projectiles;

    public GameObject chompPoint;

    public Sprite[] possibleAcidSprites;

    public Rigidbody2D Rigidbody2D;

    public float LungeCharge = 0;
    public bool IsLungeCharging = false;

    bool LungeTrigger = false;
    Vector3 LungeDir;

    private void Update()
    {
        ManageVomit();
        ManageLunge();
    }

    public void Chomp()
    {
        Collider2D hit = Physics2D.OverlapCircle(chompPoint.transform.position, 0.5f);
        if (hit.GetComponentInParent<Transform>().CompareTag("Enemy"))
        {
            hit.GetComponentInParent<Dummy>().TakeDamage(5);
        } 
    }

    public void AcidAttack()
    {
        for (int i = 0; i < Random.Range(7, 15); i++)
        {
            int spriteIndex = Random.Range(1, 3);

            Vector3 randomPos = Random.insideUnitCircle * 1;

            GameObject newAcid = Instantiate(acidPrefab, projectiles.transform);
            newAcid.transform.position = acidPoint.transform.position;
            newAcid.GetComponent<AcidController>().direction = acidEndPoint.transform.position + new Vector3(randomPos.x, randomPos.y, 0);
            newAcid.GetComponent<SpriteRenderer>().sprite = possibleAcidSprites[spriteIndex];
        }
    }

    void ManageLunge()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            LungeCharge += Time.deltaTime * 5;
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
        Rigidbody2D.velocity = transform.right * LungeCharge;
        LungeTrigger = true;
    }

    void ManageVomit()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        acidPoint.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        acidRadiusShape.SetActive(currentState == "vomitting");
    }
}
