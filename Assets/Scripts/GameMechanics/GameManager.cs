using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool isAttack = false;
    void Awake() { instance = this; }

    void Start()
    {
        Attack.attack += AttackChange;
;
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
    void AttackChange(bool attack)
    {
        isAttack = attack;
    }
    void AllowChange(bool allow)
    {
        if (!allow)
        {
            GetComponent<EnemySpawner>().StartSpawning();
            GetComponent<CloudSpawner>().StartSpawning();
        }
    }
}
