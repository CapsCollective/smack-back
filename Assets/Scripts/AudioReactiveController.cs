using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReactiveController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private Shader shader;

    private int qSamples = 256;
    float[] samples;
    public float curBassLevel;
    private float lastBassLevel;
    [SerializeField] private float bassSmoothing = 0.5f;
    [SerializeField] private float timeBetweenBeats = 0.1f;
    [SerializeField] private float bias = 0.1f;
    [SerializeField] private float fallSpeedMult = 2;

    private float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenBeats)
        {
            if (GetBass() >= bias)
            {
                lastBassLevel = GetBass();
                timer = 0;
            }
        }
        curBassLevel -= Time.deltaTime * fallSpeedMult;
        curBassLevel = Mathf.Lerp(lastBassLevel, curBassLevel, bassSmoothing);
        Shader.SetGlobalFloat("ReactiveAudio", curBassLevel);
    }

    public float[] AnalyzeSound()
    {
        float[] spectrum = new float[64];
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
