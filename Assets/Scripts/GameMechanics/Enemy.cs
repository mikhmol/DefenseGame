using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    // is this enemy target for tower now
    public bool IsTarget = false;

    //Health, Attack Power, MoveSpeed
    //public int health;
    public int attackPower;
    public float moveSpeed = 1.5f;

    private Transform target;
    private int wavepointInxed = 0;

    // all waypoints list
    List<GameObject> wayPoints = new List<GameObject> ();

    private void Start()
    {
        target = Waypoints.waypoints[0];
    }

    void Update()
    {
        Move();
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Finish")
        {
            SceneManager.LoadScene(0);
        }
    }
}