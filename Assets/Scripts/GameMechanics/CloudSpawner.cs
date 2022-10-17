using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public static CloudSpawner instance;

    //Enemy prefabs
    public List<GameObject> prefabs;
    //Enemy spawn root point
    public List<Transform> spawnPoints;
    //Enemy spawn interval
    public float spawnInterval = 2f;

    public void StartSpawning()
    {
        //Call the spawn coroutine
        StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnDelay()
    {
        //Call the spawn method
        SpawnEnemy();
        //Wait spawn interval
        yield return new WaitForSeconds(spawnInterval);
        //Recall the same coroutine
        StartCoroutine(SpawnDelay());
    }

    void SpawnEnemy()
    {
        //Randomize the enemy spawned
        int randomPrefabID = Random.Range(0, prefabs.Count);
        //Randomize the spawn point
        int randomSpawnPointID = Random.Range(0, spawnPoints.Count);
        //Instantiate the enemy prefab
        Instantiate(prefabs[randomPrefabID], spawnPoints[randomSpawnPointID]);
    }
}
