using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private ObjectPool objectPool;

    [SerializeField]
    private GameObject ship;

		[SerializeField]
		private int MaxSpawnDistance;

		[SerializeField]
		private int MinSpawnDistance;

		private GameObject[] objPool:

		private static SpawnManager instance = null;

    public void Awake()
    {
			objPool  = objectPool.getObjectPool;

			if(instance == null)
			{
				instance = this;
			}
			else if(instance != this)
			{
				Destroy(gameObject);
			}
    }

		public SpawnManager getInstance()
		{
			return instance;
		}

		public void Spawnobject()
		{
			vec3 spawnPos = calculateSpawnPos();
			objectPool.GetPooledObject(spawnPos);
		}

		public vec3 CalculateSpawnPos()
		{
			Random random = new Random();
			vec3 shipPos = ship.transform.position;
			vec3 spawnPos = new vec3(random.Next(shipPos.x + MinSpawnDistance, shipPos.x + MaxSpawnDistance) * (random.Next(0,2) * 2 -1),
															random.Next(shipPos.y + MinSpawnDistance, shipPos.y + MaxSpawnDistance) * (random.Next(0,2) * 2 -1),
															random.Next(shipPos.z + MinSpawnDistance, shipPos.z MaxSpawnDistance) * (random.Next(0,2) * 2 -1);
		  return spawnPos;
		}
}
