using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public static CloudSpawner instance;

    public GameObject cloudPrefab, cloudsFolder;

    [SerializeField] float spawnInterval = 3f;

    public void StartSpawning()
    {
        //Call the spawn coroutine
        StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnDelay()
    {
        while (true)
        { 
            //Call the spawn method
            SpawnCloud();

            //Wait spawn interval
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnCloud()
    {
        //Randomize the spawn point "y"
        float randomSpawnY = Random.Range(27.6f, 36.7f);

        //Instantiate the cloud prefab
        GameObject newCloud = Instantiate(cloudPrefab, new Vector3(-83f, randomSpawnY, 0), Quaternion.identity);
        newCloud.transform.SetParent(cloudsFolder.transform);
    }
}
