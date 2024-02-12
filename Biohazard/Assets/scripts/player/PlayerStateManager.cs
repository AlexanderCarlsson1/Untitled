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

    private void Update()
    {
        ManageVomit();
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

    public void LungeAttack()
    {
        float LungeCharge = Time.deltaTime;
        if (LungeCharge <= 5)
        {
            LungeCharge = 5;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            int Lunge = 1;
            LungeCharge += Lunge;
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Rigidbody2D.AddForce(new Vector2(angle, Lunge),ForceMode2D.Force);
        }
    }

    void ManageVomit()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        acidPoint.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        acidRadiusShape.SetActive(currentState == "vomitting");
    }
}
