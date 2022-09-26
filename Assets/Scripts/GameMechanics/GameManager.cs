using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake() { instance = this; }

    void Start()
    {
        InGameTimers.Allow += AllowChange;

        //GetComponent<HealthSystem>().Init();
        //StartCoroutine(WaveStartDelay());
    }

    /*IEnumerator WaveStartDelay()
    {
        //wait for X seconds
        yield return new WaitForSeconds(2f);
        //Start the enemy spawning
        GetComponent<EnemySpawner>().StartSpawning();
        GetComponent<CloudSpawner>().StartSpawning();
    }*/

    void AllowChange(bool allow)
    {
        if (!allow)
        {
            GetComponent<EnemySpawner>().StartSpawning();
            GetComponent<CloudSpawner>().StartSpawning();
        }
    }
}
