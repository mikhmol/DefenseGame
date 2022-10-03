using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bayraktar : CommonUnitsLogic
{
    [SerializeField] int countBuletsForShoot;
    [SerializeField] int shootedBullets;
    [SerializeField] Vector3 positionToReach;
    
    public Vector3 PositionToReach { get { return positionToReach; } set { positionToReach = value; } }

    protected override void Start()
    {
        base.Start();
        //transform.DOMove(positionToReach, Mathf.Abs(transform.position.x - positionToReach.x) / speed).SetEase(Ease.OutSine);
        transform.DOJump(positionToReach, 0.05f, (int)(Mathf.Abs(transform.position.x - positionToReach.x) / speed), Mathf.Abs(transform.position.x - positionToReach.x) / speed).SetEase(Ease.OutSine);
    }

    protected override void CheckShoot()
    {
        if (Vector2.Distance(transform.position, positionToReach) < 0.1f)
        {
            if (shootedBullets < countBuletsForShoot)
            {
                if (unitsList.Count > 0 && 
                    Time.time - timeOfLastShoot > reloadTime)
                {
                    GameObject oppositeUnit = FindMostProtected().transform.parent.gameObject;
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

                    timeOfLastShoot = Time.time;

                    shootedBullets++;
                    /*
                    shootCoroutine = Shoot(FindMostProtected().transform.parent.gameObject);
                    StartCoroutine(shootCoroutine);
                    hasTarget = true;
                    */
                }
            }
            else
            {
                Debug.Log("I leave position to reload");
                transform.DOMove(transform.position + new Vector3(10, 3, 0), 3).SetEase(Ease.Linear).OnComplete( () => { Destroy(this); Destroy(gameObject); });
            }

        }
    }
    

    GameObject FindMostProtected()
    {
        int maxHP = 0;
        GameObject MostProtectedUnit = null;
        foreach (var unit in unitsList)
        {
            if (unit.GetComponentInParent<CommonUnitsLogic>().Health > maxHP)
            {
                maxHP = unit.GetComponentInParent<CommonUnitsLogic>().Health;
                MostProtectedUnit = unit;
            }
        }
        return MostProtectedUnit;
    }

    //protected override IEnumerator Shoot()
    //{
    //    //Debug.Log("day shoot");
    //    GameObject oppositeUnit = FindMostProtected();
    //    MainShoot(oppositeUnit);
    //    //hasTarget = false;
    //    //StopCoroutine(shootCoroutine);
    //    yield return null;
    //}
}
