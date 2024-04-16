using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPuddle : MonoBehaviour
{
    Quaternion random_rotation;

    private void Start()
    {
        int randomRotationNum = Random.Range(0, 360);
        random_rotation = Quaternion.Euler(0f, 0f, randomRotationNum);
    }
    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, random_rotation, Time.deltaTime);

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (spriteRenderer.color.a <= 0.8)
        {
            spriteRenderer.color += new Color(0, 0, 0, 0.1f * Time.deltaTime);
        }

        Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.7f);
        if (hit && hit.gameObject.CompareTag("Enemy"))
        {
            hit.GetComponentInParent<EnemyClass>().TakeDamage(5);
        }
    }
}
