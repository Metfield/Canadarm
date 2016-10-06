using UnityEngine;
using System.Collections;

public class OmniPotentSphereOfSatteliteSpawning : MonoBehaviour {

	private SpawnManager SpawnManager;
	private int spawnTimer = 0;

	// Use this for initialization
	void Start () {
				spawnManager = SpawnManager.getInstance();
	}

	// Update is called once per frame
	void Update () {
			spawnTimer++;
			if(spawnTimer == 100)
			{
				spawnManager.Spawnobject();
				spawnTimer = 0;
			}
	}
}
