using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void PlayPopSFX()
    {
        Debug.Log("Pop");
        audioSource.Play();
    }
}
