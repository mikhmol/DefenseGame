using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    //Health, Attack Power, MoveSpeed
    //public int health;
    [SerializeField] float moveSpeed = 1.5f;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] int attackPower;
    [SerializeField] float Radius;
    [SerializeField] float ReloadTime;
    [SerializeField] CircleCollider2D Collider;
    List<GameObject> towers;
    List<GameObject> bullets;

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
        towers = new List<GameObject>();
        bullets = new List<GameObject>();
        Physics2D.IgnoreLayerCollision(7, 7);
        Physics2D.IgnoreLayerCollision(7, 8);
        //Physics2D.IgnoreLayerCollision(0, 0);
        //Physics2D.IgnoreLayerCollision(0, 7);
    }

    void Update()
    {
        Move();
        if (towers.Count > 0)
            Shoot(towers[Random.RandomRange(0, towers.Count)]);
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
        Debug.Log(collision.tag);
        towers.Remove(collision.gameObject);
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

        while (enemy.GetComponent<Health>().health > 0)
        {
            if (Time.time - timeOfLastShoot > ReloadTime)
            {
                int amountOfBullets = Random.Range(1, 3);
                for (int i = 0; i < amountOfBullets; i++)
                {
                    GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                    bullet.GetComponent<CollisionDamage>().collisionDamage = attackPower;
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
                    yield return new WaitForSeconds(0.1f);
                }
                //bullet.transform.position = Vector2.MoveTowards(bullet.transform.position, enemy.transform.position, BulletPrefab.GetComponent<ShootingBullet>().Speed * Time.deltaTime);
                timeOfLastShoot = Time.time;

            }
            yield return null;
        }
    }
}