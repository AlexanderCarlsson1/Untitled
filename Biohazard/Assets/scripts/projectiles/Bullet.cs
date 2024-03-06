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
}
