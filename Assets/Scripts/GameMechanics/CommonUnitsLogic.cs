using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class CommonUnitsLogic : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected BulletController bulletController;

    [SerializeField] protected int health;
    [SerializeField] protected int damage;

    // add later
    [SerializeField] protected int minNumberOfBulletsPerShoot;
    [SerializeField] protected int maxNumberOfBulletsPerShoot;
    [SerializeField] protected int damageOnUnit;
    [SerializeField] protected int damageOnTechnic;

    [SerializeField] protected float speed;
    [SerializeField] protected float viewRadius;

    [SerializeField] protected float reloadTime;
    protected bool hasTarget = false;

    protected float timeOfLastShoot;

    protected IEnumerator shootCoroutine;



    protected List<GameObject> unitsList = new List<GameObject>();

    public int Health { get => health; }

    protected  void Start()
    {
        timeOfLastShoot = Time.time;
        var view = gameObject.transform.GetComponentInChildren<CircleCollider2D>();
        if(view != null)
            view.radius = viewRadius;
        //gameObject.transform.GetChild(1).GetComponent<CircleCollider2D>().radius = viewRadius;
        Physics2D.IgnoreLayerCollision(8, 8);
        Physics2D.IgnoreLayerCollision(9, 9);
        Physics2D.IgnoreLayerCollision(10, 10);
        Physics2D.IgnoreLayerCollision(8, 10); // bullets with colliders
    }
    protected virtual void Update()
    {
        CheckShoot();
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        switch(gameObject.transform.GetChild(0).tag)
        {
            case "isUnit":  //for unit
                if (other.tag == "isEnemy")
                {
                    // other.gameObject - body of unit
                    unitsList.Add(other.gameObject);
                    Debug.Log("isEnemy");
                    //UnitDestroy();
                }
                break;
            case "isEnemy": // for enemy
                if (other.tag == "isUnit")
                {
                    unitsList.Add(other.gameObject);
                    Debug.Log("isUnit");
                }
                break;
            default:
                break;
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        switch (gameObject.transform.GetChild(0).tag)
        {
            case "isUnit": //for unit
                if (other.tag == "isEnemy")
                {
                    // other.gameObject - body of unit
                    UnitRemove(other.gameObject);
                    StopShoot();
                    Debug.Log("isEnemy exit");
                }
                break;
            case "isEnemy": // for enemy
                if (other.tag == "Finish")
                {
                    SceneManager.LoadScene(0);
                }
                if (other.tag == "isUnit")
                {
                    UnitRemove(other.gameObject);
                    StopShoot();
                    Debug.Log("isUnit exit");
                }
                break;
            default:
                break;
        }
    }
    protected virtual void CheckShoot()
    {
        if (unitsList.Count > 0 && Time.time - timeOfLastShoot > reloadTime && !hasTarget)
        {
            shootCoroutine = Shoot(unitsList[Random.Range(0, unitsList.Count)].transform.parent.gameObject);
            StartCoroutine(shootCoroutine);
            hasTarget = true;
        }
    }
    protected virtual IEnumerator Shoot(GameObject oppositeUnit)
    {
        // oppositeUnit - game object of unit 
        while (oppositeUnit.GetComponent<CommonUnitsLogic>().health > 0)
        {
            int amountOfBullets = Random.Range(minNumberOfBulletsPerShoot, maxNumberOfBulletsPerShoot);
            for (int i = 0; i < amountOfBullets; i++)
            {
                // LookAt 2D
                // get the angle
                Vector3 norTar = (oppositeUnit.transform.position - transform.position).normalized;
                float angle = Mathf.Atan2(norTar.y, norTar.x) * Mathf.Rad2Deg;
                // rotate to angle
                Quaternion rotation = new Quaternion();
                rotation.eulerAngles = new Vector3(0, 0, angle);

                GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
                Bullet bulletscr = bullet.GetComponent<Bullet>();
                bulletscr.TargetPosition = oppositeUnit.transform.position;
                bulletscr.Damage = damage;
                bulletscr.DamageOnUnit = damageOnUnit;
                bulletscr.DamageOnTechnic = damageOnTechnic;
                /*
                bullet.GetComponent<Bullet>().TargetPosition = oppositeUnit.transform.position;
                bullet.GetComponent<Bullet>().Damage = damage;
                bullet.GetComponent<Bullet>().DamageOnUnit = damageOnUnit;
                bullet.GetComponent<Bullet>().DamageOnTechnic = damageOnTechnic;
                */
                //bullet.transform.parent = transform; // made unit parant of bullet
                Physics2D.IgnoreCollision(gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>(), bullet.GetComponent<CapsuleCollider2D>());
                BulletController.bullets.Add(bullet.GetComponent<Bullet>()); // add bullet to bullet controller
                yield return new WaitForSeconds(0.1f);
                timeOfLastShoot = Time.time;
            }
            yield return new WaitForSeconds(reloadTime);
            timeOfLastShoot = Time.time;
            
        }

        
    }
    
    protected void StopShoot()
    {
        if(shootCoroutine != null)
            StopCoroutine(shootCoroutine);
        hasTarget = false;
    }

    public void GetDamage(int damage) // mb need protected
    {
        //Decrease health value
        health -= damage;
        //Blink Red animation
        StartCoroutine(BlinkRed());
        //Check if health is zero => destroy enemy
        if (health <= 0)
          Destroy(gameObject);
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
    protected void UnitDestroy(GameObject body)
    {
        // insted of unitsList[0] must be unit`s body 
        // body is unit`s body 
        Destroy(body.transform.parent.gameObject);
        UnitRemove(body);
    }
    protected void UnitRemove(GameObject body)
    {
        // body is unit`s body
        unitsList.Remove(body);
        Debug.Log("Unit removed");
    }

    protected void Move()
    {
        // override in enemy and unit to move them
    }
}
