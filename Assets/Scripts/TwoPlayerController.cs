using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TwoPlayerController : MonoBehaviour
{
    public GameObject player2;
    public GameObject banner;
    public Material onePlayerMat;
    public Material twoPlayerMat;
    public MeshRenderer screen;
    public ParticleSystem spawnParticles;
    
    private void Start()
    {
        if (ServiceLocator.Current.Get<PlayerManager>().twoPlayer) TwoPlayer = true;
    }

    public bool TwoPlayer
    {
        get => ServiceLocator.Current.Get<PlayerManager>().twoPlayer;
        set
        {
            ServiceLocator.Current.Get<PlayerManager>().twoPlayer = value;
            banner.SetActive(true);
            player2.SetActive(value);
            screen.material = value ? onePlayerMat : twoPlayerMat;
            spawnParticles.Play();
        }
    }

    public void ToggleTwoPlayer()
    {
        TwoPlayer = !TwoPlayer;
    }
}
