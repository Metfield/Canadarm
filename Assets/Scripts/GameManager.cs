using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int nbrOfInitialSatellites;

    private float time = 0;

    private int score = 0;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text endGameText;

    [SerializeField]
    private Text timer;

    [SerializeField]
    private int playTimeInSeconds;

    private float gameTime;
    private float timeLeft;

    private int x = 0;

    private bool gameOver = false;
    private bool gameStarted = false;

    public static GameManager instance = null;

    private bool timerStarted = false;

    protected int suckMyBalls = -1;

    // Use this for initialization
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // Henrik comment
        // InitSatellites();
        
        scoreText.gameObject.SetActive(false);        
        endGameText.gameObject.SetActive(false);        
    }

    public int getMyBalls()
    {
        suckMyBalls++;
        return suckMyBalls;
    }

    public void startTimer()
    {
        timerStarted = true;
        gameStarted = true;
        gameTime = Time.time;

        timeLeft = playTimeInSeconds;

        scoreText.text = "SCORE: " + score;
        scoreText.gameObject.SetActive(true);

        timer.text = "TIME LEFT: " + playTimeInSeconds;
        timer.gameObject.SetActive(true);
    }

    public bool IsGameOver()
    {
        return this.gameOver;
    }

    void InitSatellites()
    {
        while (nbrOfInitialSatellites > 0)
        {
            SpawnManager.instance.Spawnobject();
            nbrOfInitialSatellites -= 1;
        }
    }

    public void IncrementScore()
    {
        this.score += 100;
        scoreText.text = "SCORE: " + score;
    }

    public int GetScore()
    {
        return score;
    }

    void TimesUp()
    {
        SpawnManager.instance.DisableSatellites();

        //endGameText.text = "YOUR SCORE " + "\n" + score + " PTS";
        endGameText.text = "TIME'S\nUP";
        endGameText.gameObject.SetActive(true);        
        gameOver = true;
        gameStarted = false;
    }

    public void setGameStarted(bool b)
    {
        gameStarted = b;
    }

    public bool getGameStarted()
    {
        return gameStarted;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerStarted && !gameOver)
        {
            timeLeft -= Time.deltaTime;
            timer.text = "TIME LEFT: " + (int)timeLeft;

            if (timeLeft <= 10)
                timer.color = new Color(0.94f, 0.56f, 0.56f);
        }

        if (x == 100)
        {
            //Debug.Log("tick tock " + (Time.time - gameTime) + " " + playTimeInSeconds);
            if ((Time.time - gameTime >= playTimeInSeconds) && timerStarted)
            {
                Debug.Log("TIMES UP BIATCH!");
                TimesUp();
                x = 0;
            }
        }
        else
        {
            x++;

            scoreText.text = "SCORE: " + score;
            scoreText.gameObject.SetActive(true);            
        }

        if(gameOver)
        {
            if (Input.GetButton("Fire1"))
            {
                GameManager.instance = null;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                endGameText.gameObject.SetActive(false);
                gameOver = false;
                //timerStarted = false;

                NetworkManager.singleton.ServerChangeScene(SceneManager.GetActiveScene().name);
            }
        }
    }
} 