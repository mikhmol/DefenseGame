using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Health, Attack Power, MoveSpeed
    public int health, attackPower;
    public float moveSpeed;

    // wayIndex - index of road from the List, speed - moving speed of enemy
    private int wayIndex = 0;
    [SerializeField] private float speed = 10f;

    // all waypoints list
    List<GameObject> wayPoints = new List<GameObject> ();
    private void Start()
    {
        // we initialized the list of waypoints from GameController script
        wayPoints = GameObject.Find("Main Camera").GetComponent<GameController>().wayPoints; 
    }

    void Update()
    {
        Move();
    }

    //Moving forward
    void Move()
    {
        // dir - direction of enemy
        Vector3 dir = wayPoints[wayIndex].transform.position - transform.position;

        // moving gameobject by using Translate function
        transform.Translate(dir.normalized * Time.deltaTime * speed);

        // checking distance to the next waypoint
        if (Vector3.Distance(transform.position, wayPoints[wayIndex].transform.position) < 0.3f)
        {
            if (wayIndex < wayPoints.Count - 1)
            {
                wayIndex++;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
    //Lose health
    void LoseHealth()
    {
        //Decrease health value
        health--;
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
}