using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    string lastState = "idle";

    private float lastAcidUse;
    bool canUseAcid = true;

    void Update()
    {
        if (Time.time > lastAcidUse + 8)
        {
            canUseAcid = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GetComponent<PlayerStateManager>().Chomp();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && canUseAcid)
        {
            PlayerStateManager.currentState = "vomitting";
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            PlayerStateManager.currentState = "idle";

            if (lastState == "vomitting")
            {
                GetComponent<PlayerStateManager>().AcidAttack();
                lastAcidUse = Time.time;
                canUseAcid = false;
            }
        }

        lastState = PlayerStateManager.currentState;
    }
}
