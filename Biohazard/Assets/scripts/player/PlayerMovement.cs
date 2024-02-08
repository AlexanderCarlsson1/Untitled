using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed;
    public new Rigidbody2D rigidbody;
    private Vector2 moveInput;

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        rigidbody.velocity = moveInput * moveSpeed;

        if (Input.GetButtonDown("D"))
        {
            player.transform.Rotate(90, 0, 0);
        }
    }
}
