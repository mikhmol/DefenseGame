using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static int wave = 0;

    public static bool isAttack = false;
    void Awake() { instance = this; }

    void Start()
    {
        Attack.attack += AttackChange;
        InGameTimers.Allow += AllowChange;

        GetComponent<CloudSpawner>().StartSpawning();
        //GetComponent<HealthSystem>().Init();
    }

    void AttackChange(bool attack)
    {
        isAttack = attack;
    }
    void AllowChange(bool allow)
    {
        if (!allow)
        {
            GetComponent<EnemySpawner>().StartSpawning();
        }
        else 
        {
            wave++;
        }
    }
}
