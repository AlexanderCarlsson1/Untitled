using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyvision : MonoBehaviour
{
    public bool spotted = false;
    public bool search = false;
    float searchTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (search == true && spotted == false)
        {
            Search();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameObject.FindWithTag("Player"))
        {
            spotted = true;
            search = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Vector2 playerLastSeen = GameObject.FindWithTag(("Player")).GetComponent<Transform>().position;

        if (GameObject.FindWithTag("Player"))
        {
            spotted = false;
            
        }
        
        
        
    }

    void Search()
    {
        Debug.Log("searching");
    }
}
