using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TwoPlayerController : MonoBehaviour
{
    public GameObject player2;
    public GameObject banner;

    private bool twoPlayer = false;
    public bool TwoPlayer
    {
        get => twoPlayer;
        set
        {
            twoPlayer = value;
            if (banner) banner.SetActive(true);
            if (player2) player2.SetActive(value);
            player2.GetComponent<PlayerMovement>().aiControlled = !value;
        }
    }

    public void ToggleTwoPlayer()
    {
        TwoPlayer = !TwoPlayer;
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!player2) player2 = GameObject.Find("Player 2");
        player2.GetComponent<PlayerMovement>().aiControlled = !TwoPlayer;
    }
}
