using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnims : MonoBehaviour
{
    private Animator planesAnimation;

    int randomAnim = 0, prevAnim = 0;

    IEnumerator Start()
    {
        planesAnimation = GetComponent<Animator>();

        while (true)
        {
            yield return new WaitForSeconds(3);

            while (randomAnim == prevAnim) 
            {
                randomAnim = Random.Range(0, 10);
            }

            prevAnim = randomAnim;
            planesAnimation.SetInteger("PlaneIndex", randomAnim);
            planesAnimation.SetTrigger("Plane");
        }
    }
}
