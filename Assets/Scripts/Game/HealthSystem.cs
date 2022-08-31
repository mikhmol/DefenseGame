using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    //The UI text for the health count
    public Text txt_lifeCount;
    //The default value of the health count (used for init)
    public int defaultHealthCount;
    //Current health count
    public int healthCount;

    //Init the health system (reset the health count)
    public void Init()
    {
        healthCount = defaultHealthCount;
        txt_lifeCount.text =healthCount.ToString();
    }
    //Lose heath count
    public void LoseHealth()
    {
        if (healthCount < 1)
            return;

        healthCount--;
        txt_lifeCount.text = healthCount.ToString();

        CheckHealthCount();
    }

    //Check health count for losing
    void CheckHealthCount()
    {
        if(healthCount <1)
        {
            Debug.Log("You lost");
                //Call some reset values and stop the game from the manager
        }
        else
        {

        }
    }
  
}
