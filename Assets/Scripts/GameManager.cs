using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float countdown;
    public float roundTail;
    public float gameTail;

    public PlayerMovement player1;
    public PlayerMovement player2;
    public BallMovement ball;
    public float ballOffset;

    public PointsManager pointsManager1;
    public PointsManager pointsManager2;

    public GameEvent countdownEvent;
    public GameEvent playEvent;
    public GameEvent endEvent;

    private Vector3 player1Start;
    private Vector3 player2Start;
    private Vector3 ballStart;

    private float countdownStart = 0;

    public float CountdownTime { get { return countdown - (Time.timeSinceLevelLoad - countdownStart) + 1f; } }

    private void Awake()
    {
        player1Start = player1.transform.position;
        player2Start = player2.transform.position;

        pointsManager1.Reset();
        pointsManager2.Reset();

        ballStart = ball.transform.position;
    }

    private void Start()
    {
        StartCountdown();
    }

    public void StartCountdown()
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        countdownEvent.Raise();

        player1.enabled = false;
        player1.transform.position = player1Start;

        player2.enabled = false;
        player2.transform.position = player2Start;

        ball.enabled = false;
        ball.transform.forward = pointsManager2.Points > pointsManager1.Points ? Vector3.forward : Vector3.back;
        ball.transform.position = ballStart - ball.transform.forward * ballOffset;
        ball.MakeVisible();

        countdownStart = Time.timeSinceLevelLoad;
        yield return new WaitForSeconds(countdown);

        Play();
    }

    public void Play()
    {
        playEvent.Raise();

        player1.enabled = true;
        player2.enabled = true;
        ball.StartMoving();
    }

    public void EndRound()
    {
        StartCoroutine(RoundEnd());
    }

    private IEnumerator RoundEnd()
    {
        ball.Explode();

        yield return new WaitForEndOfFrame();

        if (GameEnded())
        {
            End();
        }
        else
        {
            yield return new WaitForSeconds(roundTail);
            StartCountdown();
        }
    }

    private void End()
    {
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        endEvent.Raise();

        Debug.Log((pointsManager1.Max ? "PLAYER 1" : "PLAYER 2") + " WINS! (SHOW WIN SCREEN AND PLAY WIN AUDIO HERE)");

        yield return new WaitForSeconds(gameTail);

        Debug.Log("LOAD THE MAIN MENU HERE");
    }

    private bool GameEnded()
    {
        return (pointsManager1.Max || pointsManager2.Max);
    }
}
