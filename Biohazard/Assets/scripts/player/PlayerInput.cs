using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    string lastState = "idle";
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GetComponent<PlayerStateManager>().Chomp();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            PlayerStateManager.currentState = "vomitting";
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            PlayerStateManager.currentState = "idle";

            if (lastState == "vomitting")
            {
                GetComponent<PlayerStateManager>().AcidAttack();
            }
        }

        lastState = PlayerStateManager.currentState;
    }
}
