using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    private static Transform currentTarget;

    private void Update()
    {
        ManageSearch();
    }

    void ManageSearch()
    {
        if (searching == true && spotted == false)
        {
            searchTimer += Time.deltaTime;
        }
        if (searchTimer >= 6)
        {
            searching = false;
        }
        if (searching == true && spotted == false)
        {
            Debug.Log("searching");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AIDestinationSetter destionationSetter = GetComponent<AIDestinationSetter>();

        if (collision.CompareTag("Player"))
        {
            destionationSetter.target = collision.transform;
            searching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Vector2 playerLastSeen = GameObject.FindWithTag(("Player")).GetComponent<Transform>().position;

        if (GameObject.FindWithTag("Player"))
        {
            spotted = false;
            GetComponent<AIDestinationSetter>().target = null;
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