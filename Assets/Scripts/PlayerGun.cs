using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public float fireRate;
    public Transform firingPoint;
    float lastTimeShot;

    public static PlayerGun Instance;

    // This function is to create an Instance of PlayerGun when the class PlayerGun is called
    void Awake()
    {
        Instance = GetComponent<PlayerGun>();
    }

    public void Shoot()
    {
        if (lastTimeShot + fireRate <= Time.time)
        {
            lastTimeShot = Time.time;

            // Get the bullet out of the pool and move to the position of the firing point and then shoot it
            Bullet bullet = BulletPool.instance.GetBullet(firingPoint.position, firingPoint.rotation);
            bullet.Move();
        }
    }
}
