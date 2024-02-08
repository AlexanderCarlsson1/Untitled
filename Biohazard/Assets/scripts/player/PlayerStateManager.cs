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
        if (currentState == "vomitting")
        {
            ManageVomit();
        }
    }

    public static void Chomp()
    {
        Debug.Log("Chomp");
    }

    void ManageVomit()
    {
        Debug.Log("Vomiting");
    }
}
