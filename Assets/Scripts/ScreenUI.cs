using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUI : MonoBehaviour
{
    public GameManager gameManager;

    public bool isPlayerOne;
    public Color countdownColor;

    [Header("Countdown Variables")]
    public Text countdownText;
    public AudioSource countdownOneSource;
    public AudioSource countdownTwoSource;
    public AudioSource countdownThreeSource;

    [Header("Play Variables")]
    public Text playText;

    //[Header("Score Variables")]
    //public Text scoreText;
    //[Tooltip("What is read when the player scores")] public string scoreString;

    //[Header("Concede Variables")]
    //public Text concedeText;
    //[Tooltip("What is read when the player concedes")] public string concedeString;

    [Header("End Variables")]
    public Text endText;
    public AudioSource winSource;
    public AudioClip playerOneWinClip;
    public AudioClip playerTwoWinClip;
    [Tooltip("What is read when the player has won")] [TextArea] public string winString;
    [Tooltip("What is read when the player has lost")] [TextArea] public string loseString;

    private Mode CurrentMode = Mode.None;
    public enum Mode { None, Countdown, Play, End }

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

        countdownText.color = countdownColor;
        playText.color = color;
        //scoreText.color = color;
        //concedeText.color = color;
        endText.color = color;
    }

    private void UpdateUI()
    {
        switch (CurrentMode)
        {
            case Mode.Countdown:
                UpdateCountdown();
                break;
            case Mode.Play:
                UpdatePlay();
                break;
            case Mode.End:
                UpdateEnd();
                break;
            default:
                break;
        }
    }

    public void SetMode(int newModeValue)
    {
        CurrentMode = (Mode)newModeValue;

        switch (newModeValue)
        {
            case (int)Mode.Countdown:

                countdownOneSource.PlayDelayed(2);
                countdownTwoSource.PlayDelayed(1);
                countdownThreeSource.Play();

                countdownText.enabled = true;
                playText.enabled = false;
                //scoreText.enabled = false;
                //concedeText.enabled = false;
                endText.enabled = false;
                break;

            case (int)Mode.Play:
                countdownText.enabled = false;
                playText.enabled = true;
                //scoreText.enabled = false;
                //concedeText.enabled = false;
                endText.enabled = false;
                break;

            //case (int)Mode.Score:
            //    countdownText.enabled = false;
            //    playText.enabled = false;
            //    //scoreText.enabled = true;
            //    //concedeText.enabled = false;
            //    endText.enabled = false;
            //    break;

            //case (int)Mode.Concede:
            //    countdownText.enabled = false;
            //    playText.enabled = false;
            //    //scoreText.enabled = false;
            //    //concedeText.enabled = true;
            //    endText.enabled = false;
            //    break;

            case (int)Mode.End:
                if (isPlayerOne) winSource.PlayOneShot(gameManager.pointsManager1.Max ? playerOneWinClip : playerTwoWinClip);
                countdownText.enabled = false;
                playText.enabled = false;
                //scoreText.enabled = false;
                //concedeText.enabled = false;
                endText.enabled = true;
                break;

            default:
                break;
        }
    }

    private void UpdateCountdown()
    {
        countdownText.text = ((int)gameManager.CountdownTime).ToString();
    }

    private void UpdatePlay()
    {
        playText.text = isPlayerOne ? gameManager.pointsManager1.Points.ToString() : gameManager.pointsManager2.Points.ToString();
    }

    private void UpdateEnd()
    {
        endText.text = isPlayerOne && gameManager.pointsManager1.Max || !isPlayerOne && gameManager.pointsManager2.Max ? winString : loseString;
    }
}
