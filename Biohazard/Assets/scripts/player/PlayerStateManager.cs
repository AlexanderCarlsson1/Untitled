using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public static string currentState = "idle";

    public static bool isChomping = false;

    public GameObject acidPoint;
    public GameObject acidRadiusShape;
    public GameObject acidPrefab;
    public GameObject projectiles;

    public Sprite[] possibleAcidSprites;

    private void Update()
    {
        ManageVomit();
    }

    public void Chomp()
    {
        Debug.Log("Chomp");
    }

    public void AcidAttack()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomPos = Random.insideUnitCircle * 1;

            GameObject newAcid = Instantiate(acidPrefab, projectiles.transform);
            newAcid.transform.position = acidPoint.transform.position + new Vector3(randomPos.x, randomPos.y, 0);
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
