using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

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
    private int playTimeInSeconds;

    private float gameTime; 

    public static GameManager instance = null;

    // Use this for initialization
    void Start()
    {
        gameTime = Time.time;

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        InitSatellites();
        scoreText.GetComponent<Text>();
        scoreText.text = "SCORE: " + score;

        endGameText.GetComponent<Text>();
        endGameText.gameObject.SetActive(false);
    }

    void InitSatellites()
    {
        while(nbrOfInitialSatellites > 0)
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
        gameTime = 0;
        SpawnManager.instance.DisableSatellites();
        endGameText.text = "YOUR SCORE " + "\n" + score + " PTS";
        endGameText.gameObject.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // Update is called once per frame
    void Update()
    {
        if((int)gameTime % 100 == 0)
        {
            if(Time.time - gameTime >= playTimeInSeconds)
            {
                Debug.Log("TIMES UP BIATCH!");
                TimesUp();
            }
        }
    }
}
