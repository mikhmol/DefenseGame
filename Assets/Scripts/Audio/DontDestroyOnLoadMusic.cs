using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadMusic : MonoBehaviour
{
    private void Awake()
    {
        SetMusic();
    }

    private void SetMusic()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
