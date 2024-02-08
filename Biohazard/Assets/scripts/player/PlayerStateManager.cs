using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public static string currentState = "idle";

    public static bool isChomping = false;

    public GameObject acidPoint;
    public GameObject acidRadiusShape;

    private void Update()
    {
        ManageVomit();
    }

    public static void Chomp()
    {
        Debug.Log("Chomp");
    }

    void ManageVomit()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        acidPoint.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        acidRadiusShape.SetActive(currentState == "vomitting");

        if (currentState == "vomitting")
        {
            
        }
        else
        {

        }
    }
}
