using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private ObjectPool objectPool;

    [SerializeField]
    private GameObject ship;

	[SerializeField]
	private int MaxSpawnDistance;

	[SerializeField]
	private int MinSpawnDistance;

    private GameObject[] objPool;

	public static SpawnManager instance = null;

    public void Awake()
    {
        objPool  = objectPool.GetObjectPool();

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
		Vector3 spawnPos = CalculateSpawnPos();
		objectPool.GetPooledObject(spawnPos);
	}

	public Vector3 CalculateSpawnPos()
	{
        Vector3 shipPos = ship.transform.position;
        Vector3 spawnPos;
        
        while (true)
        {
            //Vector3 spawnPos = new Vector3(Random.Range(shipPos.x + MinSpawnDistance, shipPos.x + MaxSpawnDistance) * (Random.Range(0,2) * 2 -1),
            //                                Random.Range(shipPos.y + MinSpawnDistance, shipPos.y + MaxSpawnDistance) * (Random.Range(0,2) * 2 -1),
            //                                Random.Range(shipPos.z + MinSpawnDistance, shipPos.z + MaxSpawnDistance) * (Random.Range(0,2) * 2 -1));

            spawnPos = new Vector3(Random.Range(shipPos.x - MaxSpawnDistance, shipPos.x + MaxSpawnDistance),
                                   Random.Range(shipPos.y - MaxSpawnDistance, shipPos.y + MaxSpawnDistance),
                                   Random.Range(shipPos.z - MaxSpawnDistance, shipPos.z + MaxSpawnDistance));

            //Vector3 spawnPos = new Vector3(Random.Range(shipPos.x + MinSpawnDistance, shipPos.x + MaxSpawnDistance),
            //                                 Random.Range(shipPos.y + MinSpawnDistance, shipPos.y + MaxSpawnDistance),
            //                                 Random.Range(shipPos.z + MinSpawnDistance, shipPos.z + MaxSpawnDistance));


        if (Vector3.Distance(shipPos, spawnPos) > MinSpawnDistance)
            {
                break;
            }
        }





        return spawnPos;
	}
}
