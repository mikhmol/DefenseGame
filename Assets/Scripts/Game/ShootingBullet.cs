using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBullet : MonoBehaviour
{
    [SerializeField] int BulletSpeed;
    Vector2 TargetPosition;
    public int Speed
    {
        get { return BulletSpeed; }
    }
    public Vector2 TargetPos
    {
        get { return TargetPosition; }
        set { TargetPosition = value;  }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "isEnemy")
        {
            collision.gameObject.GetComponent<Enemy>().LoseHealth();
        }
    }
    //private static void Create(Vector3 spawnPosition)
    //{
    //    Instantiate(ShootingBullet);
    //}

}
