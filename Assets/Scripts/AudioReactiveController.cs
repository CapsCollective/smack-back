using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReactiveController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private Shader shader;

    private int qSamples = 256;
    float[] samples;
    private float curBassLevel;
    private float lastBassLevel;
    private float bassSmoothing = 0.5f;
    [SerializeField] private float timeBetweenBeats = 0.1f;
    [SerializeField] private float bias = 0.1f;
    [SerializeField] private float fallSpeedMult = 2;
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;

    private float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenBeats)
        {
            timer = 0;
            if (GetBass() >= bias)
            {
                lastBassLevel = curBassLevel;
                curBassLevel = GetBass();
            }
        }
        curBassLevel = Mathf.SmoothDamp(curBassLevel, 0, ref bassSmoothing, fallSpeedMult);
        curBassLevel = Mathf.Clamp(curBassLevel, minValue, maxValue);
        Shader.SetGlobalFloat("ReactiveAudio", curBassLevel);
    }

    public float[] AnalyzeSound()
    {
        float[] spectrum = new float[256];
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);
        return spectrum;
    }

    private float GetBass()
    {
        float sum = 0;
        sum += AnalyzeSound()[0] * 100;
        return sum;
    }
}
