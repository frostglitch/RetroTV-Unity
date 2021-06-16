using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraEffectsControl : MonoBehaviour
{
    public Volume volume;

    ChromaticAberration chromatic;
    bool startChromatic;
    float chromaticSpeed;
    float chromaticOriginal;

    private void Start()
    {
        volume.profile.TryGet(out chromatic);
    }

    public void ChromaticBurst(float speed, float magnitude)
    {
        chromaticOriginal = chromatic.intensity.value;
        chromaticSpeed = speed;
        chromatic.intensity.value = magnitude;

        startChromatic = true;
    }

    private void Update()
    {
        if(startChromatic)
        {
            if(chromatic.intensity.value <= chromaticOriginal + 0.05f)
            {
                chromatic.intensity.value = chromaticOriginal;
                startChromatic = false;
            }
            else
            {
                chromatic.intensity.value = Mathf.Lerp(chromatic.intensity.value, 0f, Time.deltaTime * chromaticSpeed);
            }
        }
    }
}
