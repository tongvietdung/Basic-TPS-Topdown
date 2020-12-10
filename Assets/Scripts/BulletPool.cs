using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public float poolSize;
    public GameObject bulletPrefab;
    List<Bullet> bulletPool;

    public static BulletPool instance;

    private void Awake()
    {
        instance = GetComponent<BulletPool>();
    }
    void Start()
    {
        InitPool();
    }

    // Get bullet out of pool to use
    public Bullet GetBullet(Vector3 position, Quaternion rotation)
    {
        Bullet bullet = bulletPool[0];
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bulletPool.Remove(bullet);
        return bullet;
    }

    // Return the bullet back to pool after used
    public void ReturnToPool(Bullet bullet)
    {
        bullet.transform.position = transform.position;
        bulletPool.Add(bullet);
    }

    // Initiate the bulletPool by creating a List of Bullet and fill in with bullets
    void InitPool()
    {
        bulletPool = new List<Bullet>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bulletPool.Add(bullet.GetComponent<Bullet>());
        }
    }
}
