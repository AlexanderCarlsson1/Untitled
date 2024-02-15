using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public bool spotted = false;
    public bool search = false;
    float searchTimer = 0;
    // Start is called before the first frame update
    void Start()
    {

    }
    private static Transform currentTarget;
    // Update is called once per frame
    void Update()
    {
        if (currentTarget)
        {

        }
        if (search == true && spotted == false)
        {
            searchTimer += Time.deltaTime;
        }
        if (searchTimer >= 6)
        {
            search = false;
        }
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