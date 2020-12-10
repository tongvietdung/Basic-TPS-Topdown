using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 firingPoint;
    public float projectileSpeed;
    public float maxProjectileDistance;
    bool shouldMove = false;
    bool shouldCollide = false;
    public float damage = 20;
    private void OnCollisionEnter(Collision collision)
    {
        if (shouldCollide)
        {
            Enemy enemy = collision.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            } 

            if (collision.collider.tag == "Jumpable" || collision.collider.tag == "Ground")
            {
                shouldMove = false;
                BulletPool.instance.ReturnToPool(this);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get the position of the the firingPoint;
        firingPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            MoveProjectile();
        }
       
    }

    public void Move()
    {
        shouldMove = true;
        shouldCollide = true;
    }

    void MoveProjectile()
    {
        // When this Object is created it will move with this function
        // If the bullet move to maxDistance then destroy it
        if (Vector3.Distance(firingPoint, transform.position) > maxProjectileDistance)
        {
            BulletPool.instance.ReturnToPool(this);
            shouldMove = false;
            shouldCollide = false;
        } else
        {
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        }
    }
}
