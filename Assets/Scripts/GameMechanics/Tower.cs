using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] float Radius;
    [SerializeField] float ReloadTime;
    [SerializeField] CircleCollider2D Collider;
    List<GameObject> enemies;
    List<GameObject> bullets;

    private IEnumerator coroutine;

    private bool hasTarget = false;
    private void Start()
    {
        Collider.radius = Radius;
        enemies = new List<GameObject>();
        bullets = new List<GameObject>();
        Physics2D.IgnoreLayerCollision(7, 7);
        Physics2D.IgnoreLayerCollision(7, 8);
    }
    private void Update()
    {
        if(bullets.Count > 0)
            for (int i = 0; i < bullets.Count; i++)
            {
                var bullet = bullets[i];
                Vector2 targetPos = bullet.GetComponent<ShootingBullet>().TargetPos;
                if (Vector2.Distance(bullet.transform.position, targetPos) < 0.01f)
                {
                    Destroy(bullet);
                    bullets.Remove(bullet);
                    i++;
                    if(i < bullets.Count)
                        bullet = bullets[i];
                }
                if(bullet != null)
                    bullet.transform.position = Vector2.MoveTowards(bullet.transform.position, targetPos, BulletPrefab.GetComponent<ShootingBullet>().Speed * Time.deltaTime);
                
            }
        if (enemies.Count > 0)
        {
            if(enemies[0] == null)
                enemies.RemoveAt(0);
            foreach (var enemy in enemies)
            {
                if (!enemy.GetComponent<Enemy>().IsTarget && !hasTarget)
                {
                    hasTarget = true;
                    enemy.GetComponent<Enemy>().IsTarget = true;
                    coroutine = Shoot(enemy);
                    StartCoroutine(coroutine);
                    break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "isEnemy")
        {
            Debug.Log("enemy is here");
            enemies.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Enemy>().IsTarget = false;
        hasTarget = false;
        StopCoroutine(coroutine);
        enemies.Remove(collision.gameObject);
    }
    IEnumerator Shoot(GameObject enemy)
    {
        while (enemy.GetComponent<Enemy>().health > 0)
        {
            yield return new WaitForSeconds(ReloadTime/2);
            GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            Debug.Log(transform.position);
            bullet.GetComponent<ShootingBullet>().TargetPos = enemy.transform.position;
            bullets.Add(bullet);
            //bullet.transform.position = Vector2.MoveTowards(bullet.transform.position, enemy.transform.position, BulletPrefab.GetComponent<ShootingBullet>().Speed * Time.deltaTime);
            yield return new WaitForSeconds(ReloadTime/2);
        }
    }
}
