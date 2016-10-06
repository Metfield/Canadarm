using UnityEngine;
using System.Collections;

public class OmniPotentSphereOfSatteliteSpawning : MonoBehaviour
{

	private SpawnManager spawnManager;
	private int spawnTimer = 0;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
			spawnTimer++;
			if(spawnTimer == 100)
			{
				SpawnManager.instance.Spawnobject();
				spawnTimer = 0;
			}
	}
}
