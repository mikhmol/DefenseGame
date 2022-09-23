using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPanelsAnims : MonoBehaviour
{
    Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();
    }

    public void OpenInfoMenu()
    {
        StartCoroutine(MoveInfoPanel());
    }

    IEnumerator MoveInfoPanel()
    {
        anim.Play("openInfoPanel");

        yield return new WaitForSeconds(5f);

        anim.Play("closeInfoPanel");
    }

}
