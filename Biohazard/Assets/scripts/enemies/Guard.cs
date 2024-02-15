using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public bool spotted = false;
    public bool searching = false;

    float searchTimer = 0;

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
    }
}
