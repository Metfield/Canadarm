using UnityEngine;
using System.Collections;

public class ArmCollider : MonoBehaviour
{
    private bool isCollidingWithShip;

    [SerializeField]
    private GameObject canadarm2;

	// Use this for initialization
	void Start ()
    {
        isCollidingWithShip = false;
       
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(isCollidingWithShip)
        {
            canadarm2.GetComponent<Rigidbody>().velocity = Vector3.zero;
            canadarm2.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            canadarm2.GetComponent<Rigidbody>().isKinematic = true;
            canadarm2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; 
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("SUCK MAH COCK!");

        if(collision.gameObject.tag == "ArmShuttleCollider")
        {
            Debug.Log("MOTHAFACKA!!!");
            isCollidingWithShip = true;
        }

    }
}
