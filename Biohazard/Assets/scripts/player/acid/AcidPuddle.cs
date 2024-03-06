using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPuddle : MonoBehaviour
{
    
    private void Update()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (spriteRenderer.color.a <= 1)
        {
            spriteRenderer.color += new Color(0, 0, 0, 0.1f * Time.deltaTime);
        }

        Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.7f);
        if (hit && hit.gameObject.CompareTag("Enemy"))
        {
            hit.GetComponentInParent<Dummy>().TakeDamage(5);
        }
    }
}
