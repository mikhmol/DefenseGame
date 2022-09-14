using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private GameObject backgroundAudio;

    private void Awake()
    {
        DontDestroyOnLoad(backgroundAudio.transform.gameObject);
    }
}
