using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class AcidController : MonoBehaviour
{
    public PlayerStateManager stateManager;

    public new Rigidbody2D rigidbody;

    public GameObject acidPoint;

    public Vector3 direction;

    Quaternion random_rotation;

    public float damage = 5;
    float randomSpeed;

    bool hitSomething = false;

    private void Start()
    {
        randomSpeed = Random.Range(20, 30);

        float randomSizeNum = Random.Range(0.5f, 1.5f);

        int randomRotationNum = Random.Range(0, 360);

        transform.localScale = new Vector3(randomSizeNum, randomSizeNum, randomSizeNum);
        random_rotation = Quaternion.Euler(0f, 0f, randomRotationNum);
    }

    void Update()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (!hitSomething)
        {
            rigidbody.position = Vector3.Lerp(transform.position, direction, randomSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, random_rotation, Time.deltaTime);
        }

        if (spriteRenderer.color.a > 0.8f)
        {
            spriteRenderer.color -= new Color(0, 0, 0, 0.1f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            hitSomething = true;
        }
    }
}
