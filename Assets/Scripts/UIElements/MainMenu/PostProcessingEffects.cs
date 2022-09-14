using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingEffects : MonoBehaviour
{
    private PostProcessVolume ppVol;
    private Bloom bloom;
    private Vignette vignette;
    private ChromaticAberration chromAberration;

    private void Awake()
    {
        ppVol = GetComponent<PostProcessVolume>();
        ppVol.profile.TryGetSettings(out chromAberration);
        ppVol.profile.TryGetSettings(out bloom);
        ppVol.profile.TryGetSettings(out vignette);
    }

    private void Start()
    {
        //chromAberration.intensity.value = 1f;
        //StartCoroutine(chromAberrationOnLoad());
    }

    private void Update()
    {
        
    }

    /*IEnumerator chromAberrationOnLoad()
    {
        while (chromAberration.intensity.value != 0)
        {
            chromAberration.intensity.value -= 0.05f;
            yield return new WaitForSeconds(0.03f);
        }
    }*/
}
