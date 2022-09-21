using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameTimers : MonoBehaviour
{
    // public InGameTimers Instanse { get; set; }

    public Text NextWaweCounterText;

    [SerializeField] private float timeCount = 12;
    private bool startWave = false;

    public bool allowToSpawnUnits = true;

    private void Start()
    {
        StartCoroutine(TimeUntilWave());
    }

    private void Update()
    {
        if (timeCount <= 0 || startWave)
        {
            StopCoroutine(TimeUntilWave());
            NextWaweCounterText.text = "00:00";
            allowToSpawnUnits = false;
        }
    }

    public void FastWaveStart()
    {
        startWave = true;
    }

    IEnumerator TimeUntilWave()
    {
        while (timeCount > 0)
        {
            if (timeCount > 10)
            {
                NextWaweCounterText.text = "00:" + timeCount.ToString();
                timeCount--;
                yield return new WaitForSeconds(1f);
            }
            else if (timeCount <= 10 && timeCount > 0)
            {
                NextWaweCounterText.text = "0" + string.Format("{0:0.00}", timeCount);
                timeCount -= 0.05f;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
