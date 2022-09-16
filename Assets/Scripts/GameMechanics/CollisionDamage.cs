using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public int collisionDamage = 4;
    public string collisionTag;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == collisionTag)
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health.TakeHit(collisionDamage);
        }
    }
}
