using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMLoopedAnims : MonoBehaviour
{
    private Animator animatedDecorations;

    private int randomAnim = 0, prevAnim = 0;
    private float waitTime;

    [SerializeField] private int animsCount;

    private void Awake()
    {
        if(this.gameObject.name == "Boom")
        {
            animsCount = 20;
            waitTime = 1f;
        }
        else if(this.gameObject.name == "Fighter")
        {
            animsCount = this.gameObject.transform.childCount;
            waitTime = 3f;
        }
    }

    IEnumerator Start()
    {
        animatedDecorations = GetComponent<Animator>();

        while (true)
        {
            
            yield return new WaitForSeconds(waitTime);

            while (randomAnim == prevAnim) 
            {
                randomAnim = Random.Range(0, animsCount);
            }

            prevAnim = randomAnim;
            animatedDecorations.SetInteger("AnimIndex", randomAnim);
            animatedDecorations.SetTrigger("Anim");
        }
    }
}
