using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower2 : MonoBehaviour
{
    static bool IsAttack = false;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] int _damage;
    [SerializeField] int _health;
    [SerializeField] float Radius;
    [SerializeField] float ReloadTime;
    [SerializeField] CircleCollider2D Collider;
    List<GameObject> enemies;
    List<GameObject> bullets;

    float timeOfLastShoot;

    private IEnumerator coroutine;

    private bool hasTarget = false;
    public int Health { get { return _health; } }
    private void Awake()
    {
        float timeOfLastShoot = Time.time;
    }

    private void Start()
    {
        Collider.radius = Radius;
        enemies = new List<GameObject>();
        bullets = new List<GameObject>();
        //Physics2D.IgnoreLayerCollision(7, 7);
        //Physics2D.IgnoreLayerCollision(7, 8);
        Attack.action += StartShoot;
    }
    private void OnDestroy()
    {
        Attack.action -= StartShoot;
    }
    private void Update()
    {
        /*
        if (bullets.Count > 0)
            for (int i = 0; i < bullets.Count; i++)
            {
                var bullet = bullets[i];
                Vector2 targetPos = bullet.GetComponent<Bullet>().TargetPos;
                if (Vector2.Distance(bullet.transform.position, targetPos) < 0.01f)
                {
                    Destroy(bullet);
                    bullets.Remove(bullet);
                    i++;
                    if (i < bullets.Count)
                        bullet = bullets[i];
                }
                if (bullet != null)
                    bullet.transform.position = Vector2.MoveTowards(bullet.transform.position, targetPos, BulletPrefab.GetComponent<Bullet>().Speed * Time.deltaTime);

            }
        */
        if (enemies.Count > 0 && IsAttack)
        {
            int _randomEnemyIndex = Random.Range(0, enemies.Count);
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

        while (enemy.GetComponent<Health>().health > 0)
        {
            if (Time.time - timeOfLastShoot > ReloadTime)
            {
                GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<CollisionDamage>().collisionDamage = _damage;
                Debug.Log(transform.position);
                //bullet.GetComponent<Bullet>().Target = enemy;
                Physics2D.IgnoreCollision(bullet.GetComponent<CapsuleCollider2D>(), GetComponent<BoxCollider2D>());
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
                //bullet.transform.position = Vector2.MoveTowards(bullet.transform.position, enemy.transform.position, BulletPrefab.GetComponent<Bullet>().Speed * Time.deltaTime);
                timeOfLastShoot = Time.time;
            }
            yield return null;
        }
    }
    //Lose health
    public void LoseHealth()
    {
        //Decrease health value
        //health--;
        //Blink Red animation
        StartCoroutine(BlinkRed());
        //Check if health is zero => destroy enemy
        //if (health <= 0)
        //  Destroy(gameObject);
    }

    IEnumerator BlinkRed()
    {
        //Change the spriterendere color to red
        GetComponent<SpriteRenderer>().color = Color.red;
        //wait for really small amount of time
        yield return new WaitForSeconds(0.2f);
        //Revert to default color
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    private void StartShoot(bool sth)
    {
        IsAttack = sth;
    }
}
