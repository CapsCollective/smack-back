using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperEffect : MonoBehaviour
{
    private ParticleSystem myPS;
    private AudioSource myAS;

    private void Awake()
    {
        myPS = GetComponentInChildren<ParticleSystem>();
        myAS = GetComponent<AudioSource>();
    }

    public void Play()
    {
        myPS.Play();
        myAS.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Play();
        }
    }
}
