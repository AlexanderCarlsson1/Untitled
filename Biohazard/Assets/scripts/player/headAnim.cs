using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headAnim : MonoBehaviour
{
    public GameObject HeadStatic;
    public GameObject HeadAnim;
    private Animator ChompAnim;
    
    void Start()
    {
        ChompAnim = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (ChompAnim != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                HeadStatic.GetComponent<SpriteRenderer>().enabled = false;
                ChompAnim.SetTrigger("ChompTrigger");
            }
        }
    }
}
