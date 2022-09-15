using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingEffects : MonoBehaviour
{
    private PostProcessVolume ppVol;
    private Bloom bloom;
    private Vignette vignette;
    private ChromaticAberration chromAber;

    // animation curves for different PP effects
    [SerializeField] private AnimationCurve chromAberIntensity;
    [SerializeField] private AnimationCurve bloomIntensity;
    [SerializeField] private AnimationCurve vignetteIntensity;

    // variables for looping the animation
    private float[] currentTime = new float[3];
    private float[] totalTime = new float[3];

    private void Start()
    {
        // initializing post processing variables
        ppVol = GetComponent<PostProcessVolume>();
        ppVol.profile.TryGetSettings(out chromAber);
        ppVol.profile.TryGetSettings(out bloom);
        ppVol.profile.TryGetSettings(out vignette);

        totalTime[0] = chromAberIntensity.keys[chromAberIntensity.keys.Length - 1].time;
        totalTime[1] = bloomIntensity.keys[bloomIntensity.keys.Length - 1].time;
        totalTime[2] = vignetteIntensity.keys[vignetteIntensity.keys.Length - 1].time;
    }

    private void Update()
    {
        // chromatic aberration curve loop
        if (currentTime[0] < totalTime[0])
        {
            chromAber.intensity.value = chromAberIntensity.Evaluate(currentTime[0]);
            currentTime[0] += Time.deltaTime;
        }
        else
        {
            chromAber.intensity.value = 0;
        }

        // bloom curve loop
        bloom.intensity.value = bloomIntensity.Evaluate(currentTime[1]);
        currentTime[1] += Time.deltaTime;

        if (currentTime[1] >= totalTime[1])
        {
            currentTime[1] = 0;
        }

        // vignette curve loop
        vignette.intensity.value = vignetteIntensity.Evaluate(currentTime[2]);
        currentTime[2] += Time.deltaTime;

        if (currentTime[2] >= totalTime[2])
        {
            currentTime[2] = 0;
        }
    }
}