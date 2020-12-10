using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{
    public float spawnDelayTime = 2f;
    public int maxSpawn = 3;

    public GameObject enemyPrefab;

    Vector3 spawnPosition;

    public static RandomPosition instance;
    private void Awake()
    {
        instance = GetComponent<RandomPosition>();
    }
    private void Update()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (maxSpawn > 0)
        {
            spawnPosition = new Vector3(Random.Range(-10, 10), 5, Random.Range(-10, 10));
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            maxSpawn--;
            yield return new WaitForSeconds(spawnDelayTime);
        }
    }
}
