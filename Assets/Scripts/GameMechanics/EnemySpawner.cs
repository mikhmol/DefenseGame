using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    void Avake() { instance=this; }
    [SerializeField] int TotalEnemyCount;
    //Towercount for each type
    public List<int> EnemyCounts;
    public List<int> CurrentEnemyCounts;
    //Enemy prefabs
    public List<GameObject> prefabs;
    //Enemy spawn root point
    public List<Transform> spawnPoints;
    //Enemy spawn interval
    public float spawnInterval = 2f;

    public void StartSpawning()
    {
        CurrentEnemyCounts = new List<int>();
        for (int c = 0; c < EnemyCounts.Count; c++)
        {
            TotalEnemyCount += EnemyCounts[c];
            CurrentEnemyCounts.Add(0);
        }
        //Call the spawn coroutine
        StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnDelay()
    {
        int t = 0;
        for (int c = 0; c < CurrentEnemyCounts.Count; c++)
            t += CurrentEnemyCounts[c];
        while (t < TotalEnemyCount)
        {
            //Call the spawn method
            t += SpawnEnemy();
            Debug.Log(t);
            //Wait spawn interval
            yield return new WaitForSeconds(spawnInterval);
            //Recall the same coroutine
            //StartCoroutine(SpawnDelay());
        }
    }

    int SpawnEnemy()
    {
        //Randomize the enemy spawned
        int randomPrefabID = Random.Range(0,prefabs.Count);
        if (CurrentEnemyCounts[randomPrefabID] < EnemyCounts[randomPrefabID])
        {
            CurrentEnemyCounts[randomPrefabID]++;
            //Randomize the spawn point
            int randomSpawnPointID = Random.Range(0, spawnPoints.Count);
            //Instantiate the enemy prefab
            GameObject spawnedEnemy = Instantiate(prefabs[randomPrefabID], spawnPoints[randomSpawnPointID]);
        }
        else if(CurrentEnemyCounts[randomPrefabID] > EnemyCounts[randomPrefabID] * 0.6)
        {
            for(int c = 0; c < CurrentEnemyCounts.Count; c++)
            {
                if(EnemyCounts[c] - CurrentEnemyCounts[c] > 0)
                {
                    int randomSpawnPointID = Random.Range(0, spawnPoints.Count);
                    //Instantiate the enemy prefab
                    GameObject spawnedEnemy = Instantiate(prefabs[c], spawnPoints[randomSpawnPointID]);
                }

            }
        }
        else
            SpawnEnemy();
        return 1;
    }
}
