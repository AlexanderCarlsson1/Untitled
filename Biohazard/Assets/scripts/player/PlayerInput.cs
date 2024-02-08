using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlayerStateManager.Chomp();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            PlayerStateManager.currentState = "vomitting";
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            PlayerStateManager.currentState = "idle";
        }
    }
}
