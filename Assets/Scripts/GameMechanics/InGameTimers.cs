using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameTimers : MonoBehaviour
{
    // call Support action and Allow action
    public static Action<bool> Allow, Support;

    // text fields
    public Text NextWaweCounterText;
    public GameObject SupportHasArrived;

    // variable to track time
    [SerializeField] private float timeCount;

    private bool startWave = false, waveStarted = false;

    private void Awake()
    {
        timeCount = 1f; // there have to be 30
    }

    private void Start()
    {
        // starting two timers (one of them is visible)
        StartCoroutine(TimeUntilWave());
        StartCoroutine(TimeUntilSupport());
    }

    private void Update()
    {
        // visible timer until wave start
        if ((timeCount <= 0 || startWave) && !waveStarted)
        {
            StopCoroutine(TimeUntilWave());
            NextWaweCounterText.text = "00:00";
            waveStarted = true;
            Allow?.Invoke(false);
        }
    }

    // invisible timer until random pack of support
    IEnumerator TimeUntilSupport()
    {
        while (true)
        {
            if (Time.time > timeCount + 30f) // timer after wave started(there have to be 30f)
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(60, 121)); // random 1-2 mins time before support will arrive

                Support?.Invoke(true);

                StartCoroutine(SupportHasArrivedText());
            }

            yield return null;
        }
    }

    // timer until wave logic
    IEnumerator TimeUntilWave()
    {
        while (timeCount > 0 && !startWave)
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

    // info "Support has arrived!" text appears 
    IEnumerator SupportHasArrivedText()
    {
        SupportHasArrived.SetActive(true);
        yield return new WaitForSeconds(5f);
        SupportHasArrived.SetActive(false);
    }
}
