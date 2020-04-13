using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        GetComponent<AudioSource>().Play();
    }
}
