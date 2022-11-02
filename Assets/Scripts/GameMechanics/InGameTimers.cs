using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InGameTimers : MonoBehaviour
{
    public Spawner spawner;

    // call Support action and Allow action
    public static Action<bool> Allow, Support;

    // text fields
    public Text NextWaweCounterText;
    public Button StartNextWaveBtn;

    IEnumerator stopTimeCoroutine;

    public GameObject SupportHasArrived, spawnPoint;

    public Animator SupportClarification;

    // variable to track time
    [SerializeField] private float timeCount;

    private bool startWave = false;

    private void Start()
    {
        stopTimeCoroutine = TimeUntilWave();
        StartCoroutine(stopTimeCoroutine);
        StartNextWaveBtn.enabled = true;
    }

    public void StartNextWaveButton() 
    {
        StopCoroutine(stopTimeCoroutine);
        stopTimeCoroutine = TimeUntilWave();
        StartCoroutine(stopTimeCoroutine);

        StartNextWaveBtn.enabled = false;
        startWave = true;

        NextWaweCounterText.text = "00:00";

        Allow?.Invoke(false);
        StartCoroutine(CheckLastEnemyAlive());
    }

    // timer until wave logic
    public IEnumerator TimeUntilWave()
    {
        timeCount = 120f;

        while (timeCount > 0 && !startWave)
        {
            if (timeCount > 10) 
            {
                int min = (int)timeCount / 60;
                int sec = (int)timeCount % 60;

                if (sec >= 10)
                {
                    NextWaweCounterText.text = "0" + min.ToString() + ":" + sec.ToString();
                }
                else
                {
                    NextWaweCounterText.text = "0" + min.ToString() + ":0" + sec.ToString();
                }

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

        timeCount = 0f;
        startWave = true;

        Allow?.Invoke(false);
        StartCoroutine(CheckLastEnemyAlive());

        NextWaweCounterText.text = "00:00";
    }

    IEnumerator CheckLastEnemyAlive()
    {
        yield return new WaitForSeconds(5f);

        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            if (spawnPoint.transform.childCount == 0)
            {
                StopCoroutine(stopTimeCoroutine);
                stopTimeCoroutine = TimeUntilWave();
                StartCoroutine(stopTimeCoroutine);

                StartNextWaveBtn.enabled = true;
                startWave = false;
                Allow?.Invoke(true);
                spawner.GetSupport(true);
                ShowSupportInfo();

                break;
            }
        }
    }

    private void PushSupportClarification(bool active)
    {
        if (active)
        {
            SupportClarification.SetBool("active", true);
        }
        else
        {
            SupportClarification.SetBool("active", false);
        }
    }


    public void ShowSupportInfo() 
    {
        StartCoroutine(SupportHasArrivedText());
    }

    // info "Support has arrived!" text appears and Arestovich menu mooving with info panel 
    IEnumerator SupportHasArrivedText()
    {
        SupportHasArrived.SetActive(true);
        PushSupportClarification(true);
        yield return new WaitForSeconds(5f);
        PushSupportClarification(false);
        SupportHasArrived.SetActive(false);
    }
}