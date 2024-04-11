using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    public float speed;

    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStateManager stateManager = collision.GetComponent<PlayerStateManager>();

        if (collision.CompareTag("Player") && stateManager)
        {
            stateManager.TakeLife(1);

            Destroy(gameObject);
        }
    }
}
