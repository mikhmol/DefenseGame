using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake() { instance = this; }

    void Start()
    {
        GetComponent<HealthSystem>().Init();
        StartCoroutine(WaveStartDelay());
    }

    IEnumerator WaveStartDelay()
    {
        //wait for X seconds
        yield return new WaitForSeconds(2f);
        //Start the enemy spawning
        GetComponent<EnemySpawner>().StartSpawning();
        GetComponent<CloudSpawner>().StartSpawning();
    }
    //bool sth(bool t)
    //{
    //    if(!t)
    //    {
    //        GetComponent<EnemySpawner>().StartSpawning();
    //        GetComponent<CloudSpawner>().StartSpawning();
    //    }
    //} 
}
