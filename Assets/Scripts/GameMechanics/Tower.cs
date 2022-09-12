using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    static bool IsAttack = false;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] float Radius;
    [SerializeField] float ReloadTime;
    [SerializeField] CircleCollider2D Collider;
    List<GameObject> enemies;
    List<GameObject> bullets;

    float timeOfLastShoot;

    private IEnumerator coroutine;

    private bool hasTarget = false;

    private void Awake()
    {
        float timeOfLastShoot = Time.time;
    }

    private void Start()
    {
        Collider.radius = Radius;
        enemies = new List<GameObject>();
        bullets = new List<GameObject>();
        Physics2D.IgnoreLayerCollision(7, 7);
        Physics2D.IgnoreLayerCollision(7, 8);
        Attack.action += StartShoot;
    }
    private void OnDestroy()
    {
        Attack.action -= StartShoot;
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
        if (enemies.Count > 0 && IsAttack)
        {
            int _randomEnemyIndex = Random.RandomRange(0, enemies.Count);
            GameObject enemy = enemies[_randomEnemyIndex];
            {
                if (/*!enemy.GetComponent<Enemy>().IsTarget && */ !hasTarget)
                {
                    hasTarget = true;
                    enemy.GetComponent<Enemy>().IsTarget = true;
                    coroutine = Shoot(enemy);
                    StartCoroutine(coroutine);
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
            if (Time.time - timeOfLastShoot > ReloadTime)
            {
                GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                Debug.Log(transform.position);
                bullet.GetComponent<ShootingBullet>().TargetPos = enemy.transform.position;
                // LookAt 2D
                Vector3 target = enemy.transform.position;
                // get the angle
                Vector3 norTar = (target - transform.position).normalized;
                float angle = Mathf.Atan2(norTar.y, norTar.x) * Mathf.Rad2Deg;
                // rotate to angle
                Quaternion rotation = new Quaternion();
                rotation.eulerAngles = new Vector3(0, 0, angle);
                bullet.transform.rotation = rotation;
                bullets.Add(bullet);
                //bullet.transform.position = Vector2.MoveTowards(bullet.transform.position, enemy.transform.position, BulletPrefab.GetComponent<ShootingBullet>().Speed * Time.deltaTime);
                timeOfLastShoot = Time.time;
            }
            yield return null;
        }
    }

    private void StartShoot(bool sth)
    {
        IsAttack = sth;
    }
}
