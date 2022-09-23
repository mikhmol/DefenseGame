using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int _speed;
    [SerializeField] int _damage;
    [SerializeField] Vector2 _targetPosition;

    public int Speed { get => _speed; set => _speed = value; }
    public int Damage { get => _damage; set => _damage = value; }
    public Vector2 TargetPosition { get => _targetPosition; set => _targetPosition = value; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "isUnit" || other.tag == "isEnemy")
        {
            Debug.Log("collision with sth");
            other.transform.parent.gameObject.GetComponent<CommonUnitsLogic>().GetDamage(_damage);
        }
    }

}
