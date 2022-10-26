using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
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
        int currentWave = GameManager.wave;
        if (currentWave == 0)
        {
            EnemyCounts = EnemyWave.EnemyListStart[Random.Range(0, Supporter.SupportListStart.Count)];
        }
        else if (currentWave >= 1 && currentWave <= 5)
        {
            EnemyCounts = EnemyWave.EnemyListFirstStage[Random.Range(0, Supporter.SupportListFirstStage.Count)];
        }
        else if (currentWave >= 6 && currentWave <= 10)
        {
            EnemyCounts = EnemyWave.EnemyListSecondStagePart1[Random.Range(0, Supporter.SupportListSecondStagePart1.Count)];
        }
        else if (currentWave >= 11 && currentWave <= 15)
        {
            EnemyCounts = EnemyWave.EnemyListSecondStagePart2[Random.Range(0, Supporter.SupportListSecondStagePart2.Count)];
        }
        else if (currentWave >= 16 && currentWave <= 20)
        {
            EnemyCounts = EnemyWave.EnemyListSecondStagePart3[Random.Range(0, Supporter.SupportListSecondStagePart3.Count)];
        }
        else if (currentWave >= 21 && currentWave <= 25)
        {
            EnemyCounts = EnemyWave.EnemyListThirdStagePart1[Random.Range(0, Supporter.SupportListThirdStagePart1.Count)];
        }
        else if (currentWave >= 26 && currentWave <= 30)
        {
            EnemyCounts = EnemyWave.EnemyListThirdStagePart2[Random.Range(0, Supporter.SupportListThirdStagePart2.Count)];
        }
        else if (currentWave >= 31 && currentWave <= 35)
        {
            EnemyCounts = EnemyWave.EnemyListThirdStagePart3[Random.Range(0, Supporter.SupportListThirdStagePart3.Count)];
        }

        CurrentEnemyCounts = new List<int>();
        for (int c = 0; c < this.EnemyCounts.Count; c++)
        {
            Debug.Log(this.EnemyCounts[c]);
            TotalEnemyCount += this.EnemyCounts[c];
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
        else if(CurrentEnemyCounts[randomPrefabID] > EnemyCounts[randomPrefabID] * 0.45)
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
