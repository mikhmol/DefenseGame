using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    private void Start()
    {
        moveSpeed = Random.Range(1f, 2f);

    }

    void Update()
    {
        Move();
    }

    //Moving forward
    void Move()
    {
        // check if cloud went out of screen, its will self-destruct
        if (this.gameObject.transform.position.x >= 68f)
        {
            Destroy(this.gameObject);
        }

        this.transform.Translate(transform.right * moveSpeed * Time.deltaTime);
    }
}
