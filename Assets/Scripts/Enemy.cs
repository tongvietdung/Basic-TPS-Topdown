using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            RandomPosition.instance.maxSpawn++;
        }
    }

    private void Update()
    {
        // Fall damage
        if (transform.position.y < -5)
        {
            TakeDamage(10000);
        }
    }
    
}
