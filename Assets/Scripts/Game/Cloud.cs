using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    //Health, Attack Power, MoveSpeed
    //public int health, attackPower;
    public float moveSpeed;

    void Update()
    {
        Move();
    }

    //Moving forward
    void Move()
    {
        transform.Translate(transform.right * moveSpeed * Time.deltaTime);
    }
    //Lose health

    //void LoseHealth()
    //{
    //    //Decrease health value
    //    health--;
    //    //Blink Red animation
    //    StartCoroutine(BlinkRed());
    //    //Check if health is zero => destroy enemy
    //    if (health <= 0)
    //        Destroy(gameObject);
    //}

    //IEnumerator BlinkRed()
    //{
    //    //Change the spriterendere color to red
    //    GetComponent<SpriteRenderer>().color = Color.red;
    //    //wait for really small amount of time
    //    yield return new WaitForSeconds(0.2f);
    //    //Revert to default color
    //    GetComponent<SpriteRenderer>().color = Color.white;
    //}
}
