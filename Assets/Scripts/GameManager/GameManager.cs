using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int nbrOfInitialSatellites;

    private float time = 0;

    private int score = 0;

    public static GameManager instance = null; 

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

        InitSatellites();
    }

    void InitSatellites()
    {
        while(nbrOfInitialSatellites > 0)
        {
            SpawnManager.instance.Spawnobject();
            nbrOfInitialSatellites -= 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
