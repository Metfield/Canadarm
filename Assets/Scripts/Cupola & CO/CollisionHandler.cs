using UnityEngine;
using System.Collections;

public class CollisionHandler : MonoBehaviour {

    // Use this for initialization
    void Start()
    {


    }

   public void OnCollisonEnter(Collision col)
    {
        if(col.gameObject.CompareTag("satellite"))
        {
            col.gameObject.SetActive(false);
            SpawnManager.instance.Spawnobject();
        }
    }

	// Update is called once per frame
	void Update ()
    {
	
	}
}
