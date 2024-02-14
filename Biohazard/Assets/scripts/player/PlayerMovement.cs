using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public static float moveSpeed = 5;
    public new Rigidbody2D rigidbody;
    private Vector2 moveInput;

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        rigidbody.velocity = moveInput * moveSpeed;

        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0f, 0f, angle);

        transform.rotation = Quaternion.Lerp(transform.rotation, rot, 10 * Time.deltaTime);
    }
}