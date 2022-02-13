using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Rigidbody ball;
    public GameObject paddle1;
    public GameObject paddle2;
    public GameObject paddle3;
    public GameObject paddle4;
    //public Vector3 ballInitForce;

    public int initialVelocity;
    public float speedMultiplier;
    public int multiplierRallyCount;
    public int gameWinScore;

    public Text p1ScoreText;
    public Text p2ScoreText;
    public Text p3ScoreText;
    public Text p4ScoreText;

    public Text ControlsText;
    public Text WinningMessageText;
    public Text ContinueQuitText;

    private int rallyCount = 0;
    private int p1Score = 0;
    private int p2Score = 0;
    private int p3Score = 0;
    private int p4Score = 0;

    private bool is4PMode = false;

    private bool isSpacePressed = false;

    public bool is4PModeGame()
    {
        return is4PMode;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Launch(true));
    }

    // Update is called once per frame
    void Update()
    {
        //Relaunch on pressing 'R'
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            logBallSpeed();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            isSpacePressed = true;
        }
    }

    public IEnumerator Launch(bool isStart)
    {

        ball.velocity = new Vector3(0, 0, 0);
        rallyCount = 0;

        if (isStart)
        {
            refreshScorecard();
            resetScores();
            WinningMessageText.gameObject.SetActive(true);

            ContinueQuitText.gameObject.SetActive(true);
            ControlsText.gameObject.SetActive(true);
            isSpacePressed = false;
            yield return new WaitUntil(() => isSpacePressed == true);
            isSpacePressed = false;
            WinningMessageText.text = "";
            WinningMessageText.gameObject.SetActive(false);
            ContinueQuitText.gameObject.SetActive(false);
            ControlsText.gameObject.SetActive(false);
        }
        else
        {
            ContinueQuitText.text = ContinueQuitText.text.Replace("play,", "play again,");
        }

        refreshScorecard();
        ResetPaddles();
        ball.position = new Vector3(0, 3.5f, 0);
        int x = Random.Range(0, 2) == 0 ? -1 : 1;
        int z = Random.Range(0, 2) == 0 ? -1 : 1;
        int xzRatio = Random.Range(0, initialVelocity);

        float zVelocity = Mathf.Sqrt(initialVelocity - xzRatio);
        float xVelocity = Mathf.Sqrt(xzRatio);

        Vector3 direction = new Vector3(xVelocity * x, 0f, zVelocity * z);
        //ball.velocity += ballInitForce;
        ball.velocity += direction;

        Debug.Log("x=" + x + ", z=" + z + " xV=" + xVelocity + " xzRatio=" + xzRatio + " zV=" + zVelocity + " velocity=" + getBallSpeed());
    }

    public void logBallSpeed()
    {
        Debug.Log("Ball speed is: " + getBallSpeed() + " at " + DateTime.Now);
    }

    public float getBallSpeed()
    {
        return Mathf.Sqrt(Mathf.Pow(ball.velocity.x, 2) + Mathf.Pow(ball.velocity.z, 2));
    }

    public void resetScores()
    {
        p1Score = 0;
        p2Score = 0;
        p3Score = 0;
        p4Score = 0;
    }

    public void p1Scored()
    {
        p1Score++;
        if (p1Score + p3Score >= gameWinScore)
        {
            WinningMessageText.text = "Blue Won";
            StartCoroutine(Launch(true));
        }
        else
        {
            StartCoroutine(Launch(false));
        }
    }

    public void p2Scored()
    {
        p2Score++;
        if (p2Score + p4Score >= gameWinScore)
        {
            WinningMessageText.text = "Red Won";
            StartCoroutine(Launch(true));
        }
        else
        {
            StartCoroutine(Launch(false));
        }
    }

    public void p3Scored()
    {
        p3Score++;
        if (p1Score + p3Score >= gameWinScore)
        {
            WinningMessageText.text = "Blue Won";
            StartCoroutine(Launch(true));
        }
        else
        {
            StartCoroutine(Launch(false));
        }
    }

    public void p4Scored()
    {
        p4Score++;
        if (p2Score + p4Score >= gameWinScore)
        {
            WinningMessageText.text = "Red Won";
            StartCoroutine(Launch(true));
        }
        else
        {
            StartCoroutine(Launch(false));
        }
    }

    public void p1ScoredAgainst()
    {
        p2Score++;
        p3Score++;
        p4Score++;
        bool anyoneWon = false;
        if (p2Score >= gameWinScore)
        {
            WinningMessageText.text += "P2 Won";
            anyoneWon = true;

        }
        if (p3Score >= gameWinScore)
        {
            WinningMessageText.text += "P3 Won";
            anyoneWon = true;
        }
        if (p4Score >= gameWinScore)
        {
            WinningMessageText.text += "P4 Won";
            anyoneWon = true;
        }
        if (anyoneWon)
        {
            StartCoroutine(Launch(true));
        }
    }

    public void p2ScoredAgainst()
    {
        p1Score++;
        p3Score++;
        p4Score++;
        bool anyoneWon = false;
        if (p1Score >= gameWinScore)
        {
            WinningMessageText.text += "P1 Won";
            anyoneWon = true;
        }
        if (p3Score >= gameWinScore)
        {
            WinningMessageText.text += "P3 Won";
            anyoneWon = true;
        }
        if (p4Score >= gameWinScore)
        {
            WinningMessageText.text += "P4 Won";
            anyoneWon = true;
        }
        if (anyoneWon)
        {
            StartCoroutine(Launch(false));
        }
    }

    public void p3ScoredAgainst()
    {
        p1Score++;
        p2Score++;
        p4Score++;
        bool anyoneWon = false;
        if (p1Score >= gameWinScore)
        {
            WinningMessageText.text += "P1 Won";
            anyoneWon = true;
        }
        if (p2Score >= gameWinScore)
        {
            WinningMessageText.text += "P2 Won";
            anyoneWon = true;
        }
        if (p4Score >= gameWinScore)
        {
            WinningMessageText.text += "P4 Won";
            anyoneWon = true;
        }
        if (anyoneWon)
        {
            StartCoroutine(Launch(false));
        }

    }

    public void p4ScoredAgainst()
    {
        p1Score++;
        p2Score++;
        p3Score++;
        bool anyoneWon = false;
        if (p1Score >= gameWinScore)
        {
            WinningMessageText.text += "P1 Won";
            anyoneWon = true;
        }
        if (p2Score >= gameWinScore)
        {
            WinningMessageText.text += "P2 Won";
            anyoneWon = true;
        }
        if (p3Score >= gameWinScore)
        {
            WinningMessageText.text += "P3 Won";
            anyoneWon = true;
        }
        if (anyoneWon)
        {
            StartCoroutine(Launch(false));
        }
    }

    public void refreshScorecard()
    {
        if (is4PMode)
        {
            p1ScoreText.text = "Left: " + p1Score;
            p2ScoreText.text = "Top: " + p2Score;
            p3ScoreText.text = "Right: " + p3Score;
            p4ScoreText.text = "Bottom: " + p4Score;
        }
        else
        {
            int blueScore = p1Score + p3Score;
            int redScore = p2Score + p4Score;

            p1ScoreText.text = "Blue: " + blueScore;
            p2ScoreText.text = "Red: " + redScore;
            p3ScoreText.text = "Blue: " + blueScore;
            p4ScoreText.text = "Red: " + redScore;
        }
    }

    public void ResetPaddles()
    {
        paddle1.GetComponent<Paddle>().ResetPaddle();
        paddle2.GetComponent<Paddle>().ResetPaddle();
        paddle3.GetComponent<Paddle>().ResetPaddle();
        paddle4.GetComponent<Paddle>().ResetPaddle();
    }

    public void increaseRallyAndSpeedIfNeeded() {
        rallyCount++;

        if(rallyCount % multiplierRallyCount == 0) {
            increaseBallSpeed();
        }
    }

    public void increaseBallSpeed() {
        //float ball_speed = getBallSpeed();
        //float fractionIncrease = 1.0f + speedMultiplier/ball_speed;
        //ball.velocity = new Vector3(ball.velocity.x * fractionIncrease, 0, ball.velocity.z * fractionIncrease);


        ball.velocity = new Vector3(ball.velocity.x * speedMultiplier, 0, ball.velocity.z * speedMultiplier);
    }
}
