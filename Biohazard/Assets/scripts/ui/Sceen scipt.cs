using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceen : MonoBehaviour
{
    private void Update()
    {
        bool click = Input.GetMouseButtonDown(0);

        if (click)
        {
            SceneManager.LoadScene("Nr1 rum");
        }
    }
}
