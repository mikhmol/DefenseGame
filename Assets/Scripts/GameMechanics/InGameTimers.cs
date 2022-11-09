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
    IEnumerator checkLastEnemyAlive;
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
        //StopCoroutine(stopTimeCoroutine);
        
        stopTimeCoroutine = TimeUntilWave();
        StartCoroutine(stopTimeCoroutine);
        

        StartNextWaveBtn.enabled = false;
        startWave = true;

        NextWaweCounterText.text = "00:00";

        Allow?.Invoke(false);
        if(checkLastEnemyAlive != null)
            StopCoroutine(checkLastEnemyAlive);
        checkLastEnemyAlive = CheckLastEnemyAlive();
        StartCoroutine(checkLastEnemyAlive);
    }

    // timer until wave logic
    
    public IEnumerator TimeUntilWave()
    {
        timeCount = 5f;
        Debug.Log("time count = " + timeCount);
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

        //timeCount = 0f;
        startWave = true;
        Debug.Log("from timer");
        Allow?.Invoke(false);
        if(checkLastEnemyAlive != null)
            StopCoroutine(checkLastEnemyAlive);
        checkLastEnemyAlive = CheckLastEnemyAlive();
        StartCoroutine(checkLastEnemyAlive);



        NextWaweCounterText.text = "00:00";
    }
    
    IEnumerator CheckLastEnemyAlive()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("from check");
        while (true)
        {
            yield return new WaitForSeconds(0.01f);

            if (spawnPoint.transform.childCount == 0)
            {
                StartNextWaveBtn.enabled = true;
                startWave = false;

                StopCoroutine(stopTimeCoroutine);
                stopTimeCoroutine = TimeUntilWave();
                StartCoroutine(stopTimeCoroutine);

                

                
                Allow?.Invoke(true);
                spawner.GetSupport(true);
                ShowSupportInfo();
                Debug.Log("All were killed");
                /*
                StopCoroutine(checkLastEnemyAlive);
                checkLastEnemyAlive = CheckLastEnemyAlive();
                StartCoroutine(checkLastEnemyAlive);
                */
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