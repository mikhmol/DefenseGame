using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CommonUnitsLogic
{
    [SerializeField] float stanTime = 1f;
    [SerializeField] float XPositionOfLevelEnd = -16f;
    bool isFirstShoot = false;
    protected override void Start()
    {
        base.Start();
        Attack.attack += AfterFirstShoot;
    }
    protected override void Update()
    {
        base.Update();
        if (transform.position.x < -15)
            Debug.Log("YOU ARE LOOOOOOSER");
    }

    void AfterFirstShoot(bool attack)
    {
        if (!isFirstShoot)
        {
            isFirstShoot = attack;
            // змінити іконки техніки на стан
            Invoke("Stan", stanTime);
            //StartCoroutine(Stan());
        }
    }

    //IEnumerator
    void Stan()
    {
        //yield return new WaitForSeconds(stanTime);
        Vector3 targetPos = transform.position + FindSafePosition();
        transform.DOMove(targetPos, Vector3.Distance(targetPos, transform.position) / speed).SetEase(Ease.Linear).OnComplete(() => 
        {
            transform.DOMoveX(XPositionOfLevelEnd, Mathf.Abs(XPositionOfLevelEnd - transform.position.x) / speed);
        });
    }
    Vector3 FindSafePosition()
    {
        Vector3 dir = new Vector3(0, 0, 0);
        int safetyIndex = 0;
        for(int x = -2; x < 1; x++)
        {
            for(int y = -2; y < 3; y++)
            {
                int safe = Random.Range(0, 4);
                if (safe > safetyIndex) // insted random get safety index of tile
                {
                    safetyIndex = safe;
                    dir = new Vector3(x, y, 0);
                }
            }
        }
        return dir;
    }
    /*
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "isUnit")
        {
            Debug.Log("isUnit");
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "isUnit")
        {
            Debug.Log("isUnit exit");
        }
    }
    */
}
