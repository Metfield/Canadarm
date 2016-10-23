using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Object pool for game objects.
/// Objects are considered to be in the pool if
/// they are inactive in the scene hierarchy.
/// </summary>
public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject m_pooledObject;

    [SerializeField]
    private int m_poolSize;

    private GameObject[] m_GameObjPool;
    //private List<GameObject> m_GameObjPool;

    private void Awake()
    {
        // Fill the pool.
        m_GameObjPool = new GameObject[m_poolSize];
        //m_GameObjPool = new List<GameObject>();
        for(int i = 0; i < m_poolSize; i++)
        {
            GameObject go = Instantiate(m_pooledObject) as GameObject;
            go.SetActive(false);
            m_GameObjPool[i] = go;
            //m_GameObjPool.Add(go);
        }
    }

	public GameObject[] GetObjectPool()
	{
		return m_GameObjPool;
	}

    /// <summary>
    /// Get an object from the pool.
    /// <para>
    /// Returned object is still inactive in the scene
    /// and considered to be in the pool untill it is activated.
    /// </para>
    /// Will return null if the pool is empty.
    /// </summary>
    /// <returns>A game object, null if none is available</returns>
    public void GetPooledObject(Vector3 spawnPos, Vector3 acceleration)
    {

            GameObject pooledObj = null;
            for (int i = 0; i < m_poolSize; i++)
            {
                if (!m_GameObjPool[i].activeInHierarchy)
                {
                    pooledObj = m_GameObjPool[i];
                    i = m_poolSize;
                }
            }
		if (m_GameObjPool.Length > 0) {
			pooledObj.transform.position = spawnPos;
			pooledObj.GetComponent<SatelliteBehaviour> ().SetAcceleration (acceleration);
			{

			}
			pooledObj.SetActive (true);
		}

    }

    public void Reset()
    {
        for (int i = 0; i < m_poolSize; i++)
        {
            m_GameObjPool[i].SetActive(false);
        }
    }
}
