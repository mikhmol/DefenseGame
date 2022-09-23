using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationsOnStart : MonoBehaviour
{
    Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();
        anim.Play("movePlayButton");
        //anim.PlayQueued("moveSettingsButton", QueueMode.CompleteOthers);
    }

    public void OnTriggered(int animID)
    {
        if(animID == 1)
        {
            anim.Play("moveSettingsButton");
        }
        else if(animID == 2)
        {
            anim.Play("moveQuitButton");
        }
    }
}
