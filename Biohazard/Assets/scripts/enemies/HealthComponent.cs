using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float health = 10;

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);

        if (health <= 0)
        {
            Debug.Log("Dummy died");
            Destroy(gameObject);
        }
    }
}
