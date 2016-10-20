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

    [SerializeField]
    private float SlowerThanShip;

    [SerializeField]
    private int delaySecondsRespawn;

    private GameObject[] objPool;

    private float time;

    private bool respawnPending = false;

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

	public void Spawnobject()
	{
		Vector3 spawnPos = CalculateSpawnPos();
    Vector3 acceleration = CalculateAcceleration();
		objectPool.GetPooledObject(spawnPos, acceleration);
	}

    public void ReSpawnObject()
    {
        this.time = Time.time;
        respawnPending = true;
    }

    public void Update()
    {
        if(respawnPending)
        {
            if (Time.time - time < delaySecondsRespawn)
            {
                Spawnobject();
                respawnPending = false;
            }
        }
    }

	public Vector3 CalculateSpawnPos()
	{
    Vector3 shipPos = ship.transform.position;
    Vector3 spawnPos;

    // generates a random 3D point in Polar coordiantes
    int radius = Random.Range(MinSpawnDistance, MaxSpawnDistance);
    float theta = Random.Range(0, 2*Mathf.PI);
    float phi = Random.Range(-Mathf.PI/2, Mathf.PI/2);

    return PolarToCartesian(radius, theta, phi);
	}

  // Converts Polar coordinates to cartesian.
  public Vector3 PolarToCartesian(int radius, float theta, float phi)
  {
    Vector3 shipPos = ship.transform.position;
    Vector3 spawnPos = new Vector3(radius * Mathf.Cos(theta) * Mathf.Cos(phi) + shipPos.x,
                           radius * Mathf.Sin(phi) + shipPos.y,
                           radius * Mathf.Sin(theta) * Mathf.Cos(phi) + shipPos.z);
  return spawnPos;
  }

  public Vector3 CalculateAcceleration()
  {
    float maxSpeed = ship.GetComponent<SpaceShuttle>().GetMaxSpeed();
    Vector3 acceleration = new Vector3(Random.Range(0, maxSpeed * SlowerThanShip), Random.Range(0, maxSpeed * SlowerThanShip), Random.Range(0, maxSpeed * SlowerThanShip));
    return acceleration;
  }

  public void DisableSatellites()
    {
        objectPool.Reset();
    }
}
