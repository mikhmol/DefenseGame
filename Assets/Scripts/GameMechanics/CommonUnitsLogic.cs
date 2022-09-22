using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommonUnitsLogic : MonoBehaviour
{
    bool hasTarget = false;
    //Health, Attack Power, MoveSpeed
    [SerializeField] public int health;
    [SerializeField] float moveSpeed = 1.5f;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] int attackPower;
    [SerializeField] int attackPowerOnUnit;
    [SerializeField] int attackPowerOnMachine;
    [SerializeField] float Radius;
    [SerializeField] float ReloadTime;
    [SerializeField] CircleCollider2D Collider;
    List<GameObject> towers = new List<GameObject>();
    List<GameObject> bullets = new List<GameObject>();


    float timeOfLastShoot;

    private IEnumerator coroutine;

    private Transform target;

    private void Start()
    {
        timeOfLastShoot = Time.time;
        Collider.radius = Radius;
        Physics2D.IgnoreLayerCollision(7, 7);
        Physics2D.IgnoreLayerCollision(8, 8);
        Physics2D.IgnoreLayerCollision(0, 0);
        Physics2D.IgnoreLayerCollision(0, 8);
        Physics2D.IgnoreLayerCollision(8, 7);
    }

    void Update()
    {
        StartShoot();
    }
    private void OnDestroy()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(gameObject.tag) // tag of enemy or unit
        {
            case "isUnit":
                if (other.tag == "isEnemy")
                {
                    Debug.Log("enemy is here");
                    towers.Add(other.gameObject);
                    
                }
                break;
            case "isEnemy":
                if (other.tag == "Finish")
                {
                    SceneManager.LoadScene(0);
                }
                if (other.tag == "isUnit")
                    towers.Add(other.gameObject);
                break;
        }
        Debug.Log(other.tag);
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        if (collision.gameObject.tag == "isUnit")
        {
            towers.Remove(collision.gameObject);
            StopCoroutine(coroutine);
            hasTarget = false;
        }

    }
   
    void DestroyAllBullets() // when unit killed destroed all bullets;
    {
        foreach (GameObject b in bullets)
            Destroy(b);
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
    void StartShoot()
    {
        if (towers.Count > 0 && Time.time - timeOfLastShoot > ReloadTime && !hasTarget)
        {
            coroutine = Shoot(towers[Random.Range(0, towers.Count)]);
            StartCoroutine(coroutine);
            hasTarget = true;
        }
    }
    IEnumerator Shoot(GameObject enemy)
    {
        int hp = 0;
        try
        {
            hp = enemy.GetComponent<Tower2>().Health;
        }
        catch (System.Exception)
        {
            try
            {
                hp = enemy.GetComponent<Tower>().Health;
            }
            catch (System.Exception)
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
                    bullet.transform.parent = transform; // made unit parant of bullet
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