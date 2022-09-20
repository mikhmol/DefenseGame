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
        Debug.Log("bullet colision " + collision.tag);
        if (collision.gameObject.tag == "isEnemy")
        {
            collision.gameObject.GetComponent<Enemy>().LoseHealth();
        }
        else if(collision.gameObject.tag == "isUnit")
        {
            GameObject enemy = collision.gameObject;
            try
            {
                enemy.GetComponent<Tower2>().LoseHealth();
            }
            catch (System.Exception)
            {
                try
                {
                    enemy.GetComponent<Tower>().LoseHealth();
                }
                catch (System.Exception)
                {

                }
            }
        }
    }
    //private static void Create(Vector3 spawnPosition)
    //{
    //    Instantiate(ShootingBullet);
    //}

}
