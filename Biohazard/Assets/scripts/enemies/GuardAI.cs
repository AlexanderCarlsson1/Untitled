using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GuardAI : MonoBehaviour
{
    public LayerMask SightCollideLayers;

    public GameObject projectilesContainer;
    public GameObject target;
    public GameObject bullet;

    public Rigidbody2D rigidBody;

    public float shootCooldown = 1;
    public float speed;
    float lastShot;

    private void Update()
    {
        CheckTarget();
        ManageShooting();
    }

    void ManageShooting()
    {
        if (Time.time > lastShot + shootCooldown && target)
        {
            if (!TargetIsInSight(target.transform))
                return;

            GameObject newBullet = Instantiate(bullet, projectilesContainer.transform);
            newBullet.transform.position = transform.position;
            newBullet.transform.rotation = transform.rotation;

            lastShot = Time.time;
        }
    }

    void CheckTarget()
    {
        if (!target)
            return;

        Vector3 look = transform.InverseTransformPoint(target.transform.position);
        float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;

        transform.Rotate(0, 0, angle);

        rigidBody.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (!TargetIsInSight(collision.transform))
            return;

        Debug.Log("player found!");

        target = collision.gameObject;
    }

    bool TargetIsInSight(Transform trans)
    {
        Vector2 dir = trans.position - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 100, SightCollideLayers.value);
        
        if (hit)
            return false;

        return true;
    }
}
