using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    bool hasTarget = false;
    //Health, Attack Power, MoveSpeed
    //public int health;
    [SerializeField] float moveSpeed = 1.5f;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] int attackPower;
    [SerializeField] float Radius;
    [SerializeField] float ReloadTime;
    [SerializeField] CircleCollider2D Collider;
    List<GameObject> towers = new List<GameObject>();
    List<GameObject> bullets = new List<GameObject>();

    float timeOfLastShoot;

    private IEnumerator coroutine;

    // is this enemy target for tower now
    public bool IsTarget = false;

    

    private Transform target;
    private int wavepointInxed = 0;

    // all waypoints list
    List<GameObject> wayPoints = new List<GameObject> ();

    private void Start()
    {
        target = Waypoints.waypoints[0];
        float timeOfLastShoot = Time.time;
        Collider.radius = Radius;
        Physics2D.IgnoreLayerCollision(7, 7);
        Physics2D.IgnoreLayerCollision(8, 8);
        Physics2D.IgnoreLayerCollision(0, 0);
        //Physics2D.IgnoreLayerCollision(0, 7);
    }

    void Update()
    {
        Move();
        if (towers.Count > 0 && Time.time - timeOfLastShoot > ReloadTime && !hasTarget)
        {
            //Debug.Log(towers[Random.RandomRange(0, towers.Count)]);
            coroutine = Shoot(towers[Random.Range(0, towers.Count)]);
            StartCoroutine(coroutine);
            hasTarget = true;
        }
        /*
        if (bullets.Count > 0)
            for (int i = 0; i < bullets.Count; i++)
            {
                var bullet = bullets[i];
                Vector2 targetPos = bullet.GetComponent<ShootingBullet>().TargetPos;
                if (Vector2.Distance(bullet.transform.position, targetPos) < 0.01f)
                {
                    Destroy(bullet);
                    bullets.Remove(bullet);
                    i++;
                    if (i < bullets.Count)
                        bullet = bullets[i];
                }
                if (bullet != null)
                    bullet.transform.position = Vector2.MoveTowards(bullet.transform.position, targetPos, BulletPrefab.GetComponent<ShootingBullet>().Speed * Time.deltaTime);

            }
        */
    }
    private void OnDestroy()
    {
        Debug.Log("enemy cild");
        foreach(GameObject b in bullets) 
            Destroy(b);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Finish")
        {
            SceneManager.LoadScene(0);
        }
        if(other.gameObject.tag == "isUnit")
            towers.Add(other.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        if(collision.gameObject.tag == "isUnit")
        {
            towers.Remove(collision.gameObject);
            StopCoroutine(coroutine);
            hasTarget = false;
        }
        
    }
    // Enemy moving method
    void Move()
    {
        // We are getting direction of the next waypoint and moving it
        Vector3 dir =  target.position - transform.position;
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);
    
        // if the enemy reached waypoint, its going to the next one
        if(Vector3.Distance(transform.position, target.position) <= 0.05f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        // if enemy reached the last waypoint - it's destroying
        if(wavepointInxed >= Waypoints.waypoints.Length - 1)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene(0);
            return;
        }

        wavepointInxed++;
        target = Waypoints.waypoints[wavepointInxed];
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

    IEnumerator Shoot(GameObject enemy)
    {
        int hp = 0;
        try 
        {
            hp = enemy.GetComponent<Tower2>().Health;
        }
        catch(System.Exception)
        {
            try
            {
                hp = enemy.GetComponent<Tower>().Health;
            }
            catch(System.Exception)
            {

            }
        }
        while (hp > 0)
        {
                if (Time.time - timeOfLastShoot > ReloadTime)
                {
                    int amountOfBullets = Random.Range(3, 6);
                    for (int i = 0; i < amountOfBullets; i++)
                    {
                        GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                        bullet.GetComponent<CollisionDamage>().collisionDamage = attackPower;
                        //Debug.Log(transform.position);
                        bullet.GetComponent<ShootingBullet>().Target = enemy;
                        bullet.GetComponent<ShootingBullet>().TargetPos = enemy.transform.position;
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
                        yield return new WaitForSeconds(0.1f);
                    }
                    //bullet.transform.position = Vector2.MoveTowards(bullet.transform.position, enemy.transform.position, BulletPrefab.GetComponent<ShootingBullet>().Speed * Time.deltaTime);
                    timeOfLastShoot = Time.time;

                }
                yield return null;
        }
    }
}



