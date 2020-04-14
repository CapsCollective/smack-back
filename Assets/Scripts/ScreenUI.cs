using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUI : MonoBehaviour
{
    public GameManager gameManager;
    public AudioSource audioSource;

    public bool isPlayerOne;

    [Header("Countdown Variables")]
    public Text countdownText;
    public AudioClip countdownClip;

    [Header("Play Variables")]
    public Text playText;

    [Header("Score Variables")]
    public Text scoreText;
    [Tooltip("What is read when the player scores")] public string scoreString;
    [Tooltip("What is read when the player concedes")] public string concedeString;

    [Header("End Variables")]
    public Text endText;
    [Tooltip("What is read when the player has won")] public string winString;
    [Tooltip("What is read when the player has lost")] public string loseString;

    private Mode CurrentMode = Mode.None;
    public enum Mode { None, Countdown, Play, Score, End }

    private void Start()
    {
        SetColors();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void SetColors()
    {
        Color color = isPlayerOne ? ServiceLocator.Current.Get<PlayerManager>().GetPlayerColor(PlayerMovement.PlayerNumber.One) : ServiceLocator.Current.Get<PlayerManager>().GetPlayerColor(PlayerMovement.PlayerNumber.Two);

        countdownText.color = color;
        playText.color = color;
        scoreText.color = color;
        endText.color = color;
    }

    private void UpdateUI()
    {
        switch (CurrentMode)
        {
            case Mode.Countdown:
                UpdateCountdown();
                break;
            case Mode.End:
                UpdateEnd();
                break;
            case Mode.Play:
                UpdatePlay();
                break;
            case Mode.Score:
                UpdateScore();
                break;
            default:
                break;
        }
    }

    public void SetMode(int newModeValue) { CurrentMode = (Mode)newModeValue; }

    private void UpdateCountdown()
    {
        countdownText.text = ((int)gameManager.CountdownTime).ToString();
    }

    private void UpdateEnd() { }

    private void UpdatePlay() { }

    private void UpdateScore() { }
}
