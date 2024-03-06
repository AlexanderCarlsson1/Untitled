using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    AIDestinationSetter destionationSetter;

    public GameObject projectilesContainer;
    public GameObject bullet;

    public float shootCooldown = 1;

    public LayerMask wallMask;

    private static Transform currentTarget;

    private bool searching;

    private float searchTimer;
    float lastShot;

    private void Start()
    {
        destionationSetter = GetComponent<AIDestinationSetter>();
    }

    private void Update()
    {
        ManageShooting();
        ManageSearch();

        Transform target = destionationSetter.target;

        if (target)
        {
            CheckSight(target);

            Vector3 look = transform.InverseTransformPoint(target.position);
            float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;

            transform.Rotate(0, 0, angle);
        }
    }

    void ManageShooting()
    {
        if (Time.time > lastShot + shootCooldown && destionationSetter.target)
        {
            GameObject newBullet = Instantiate(bullet, projectilesContainer.transform);
            newBullet.transform.position = transform.position;
            newBullet.transform.rotation = transform.rotation;

            lastShot = Time.time;
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

    void CheckSight(Transform trans)
    {
        RaycastHit2D result = Physics2D.Raycast(transform.position, trans.position, 100f, wallMask.value);

        if (result)
        {
            destionationSetter.target = null;
            searching = false;

            return;
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