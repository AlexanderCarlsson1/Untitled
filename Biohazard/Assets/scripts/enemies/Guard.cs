using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    AIDestinationSetter destionationSetter;

    private static Transform currentTarget;

    private bool searching;

    private float searchTimer;

    private void Start()
    {
        destionationSetter = GetComponent<AIDestinationSetter>();
    }

    private void Update()
    {
        ManageSearch();

        Transform target = destionationSetter.target;

        if (target)
        {
            Vector3 look = transform.InverseTransformPoint(target.position);
            float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;

            transform.Rotate(0, 0, angle);
        }
    }

    void ManageSearch()
    {
        if (searching == false)
        {
            destionationSetter.target = null;
            searchTimer += Time.deltaTime;
        }
        if (searchTimer >= 6)
        {
            // go back to original location?
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            destionationSetter.target = collision.transform;
            searching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            searching = false;
        }
    }
}