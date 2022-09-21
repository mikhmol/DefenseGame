using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBullet : MonoBehaviour
{
    [SerializeField] int BulletSpeed;
    GameObject _Target;
    [SerializeField] Vector2 TargetPosition;
    public int Speed
    {
        get { return BulletSpeed; }
    }
    public Vector2 TargetPos
    {
        get { return TargetPosition; }
        set { TargetPosition = value; }
    }
    public GameObject Target
    {
        get { return _Target; }
        set { _Target = value;  }
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, Speed * Time.deltaTime);
        if (_Target != null)
        {
            if (Vector2.Distance(transform.position, _Target.transform.position) < 0.2f)
            {
                try
                {
                    _Target.GetComponent<Enemy>().LoseHealth();
                }
                catch
                {
                    try
                    {
                        _Target.GetComponent<Tower2>().LoseHealth();
                    }
                    catch
                    {
                        try
                        {
                            _Target.GetComponent<Tower>().LoseHealth();
                        }
                        catch
                        {

                        }
                    }
                }
                Destroy(gameObject);
            }
            else if (Vector2.Distance(transform.position, TargetPosition) < 0.1f)
                Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("bullet colision " + collision.tag);
        if (collision.gameObject.tag == "isEnemy")
        {
            collision.gameObject.GetComponent<Enemy>().LoseHealth();
        }
        else if(collision.gameObject.tag == "isUnit")
        {
            GameObject enemy = collision.gameObject;
            try
            {
                enemy.GetComponent<Tower2>().LoseHealth();
            }
            catch (System.Exception)
            {
                try
                {
                    enemy.GetComponent<Tower>().LoseHealth();
                }
                catch (System.Exception)
                {

                }
            }
        }
    }
    */
    //private static void Create(Vector3 spawnPosition)
    //{
    //    Instantiate(ShootingBullet);
    //}

}
