using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    static public List<Bullet> bullets = new List<Bullet>();
    
    List<Bullet> bulletsToDestroy = new List<Bullet>();
    // Update is called once per frame
    void Update()
    {
        if(bullets.Count > 0)
        {
            foreach (Bullet bullet in bullets)
            {
                if (Vector2.Distance(bullet.gameObject.transform.position, bullet.TargetPosition) < 0.01f)
                {
                    bulletsToDestroy.Add(bullet);
                }
                else
                {
                    bullet.transform.position = Vector2.MoveTowards(bullet.transform.position, bullet.TargetPosition, bullet.Speed * Time.deltaTime);
                    //bullet.transform.position += bullet.transform.forward * bullet.Speed * Time.deltaTime;
                }
            }
            foreach(Bullet bullet in bulletsToDestroy)
            {
                Destroy(bullet.gameObject);
                bullets.Remove(bullet);               

            }
            bulletsToDestroy.Clear();
        }
    }
}
