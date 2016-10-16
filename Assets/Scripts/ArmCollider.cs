using UnityEngine;
using System.Collections;

public class ArmCollider : MonoBehaviour
{
    private static bool mIsArmCollidingWithShip;
    private static float mLastAxisValue;
    private static bool mIsColliderCollidingWithShip;

    [SerializeField]
    private GameObject canadarm2;


	// Use this for initialization
	void Start ()
    {
        mIsArmCollidingWithShip = false;
        mLastAxisValue = 0;
        mIsColliderCollidingWithShip = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(mIsArmCollidingWithShip)
        {
            canadarm2.GetComponent<Rigidbody>().velocity = Vector3.zero;
            canadarm2.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            canadarm2.GetComponent<Rigidbody>().isKinematic = true;
            canadarm2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; 
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("SUCK MAH COLLISION!" + collision.gameObject.tag);

        if(collision.gameObject.tag == "ArmShuttleCollider")
        {
            //Debug.Log("MOTHAFACKA!!!");
            mIsArmCollidingWithShip = true;

            // STOP MOMENTUM
        }

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("SUCK MAH TRIGGER! " + other.tag);
        
        if (other.tag == "ArmShuttleCollider")
        {
            //Debug.Log("MOTHAFACKA!!!");

            mLastAxisValue = Input.GetAxis("Z Axis");
            mIsArmCollidingWithShip = true;
        }   

        if (other.tag == "ArmShuttleCollider")
        {
            mIsColliderCollidingWithShip = true;
        }
    }

    public static bool isArmCollidingWithShip()
    {
        return mIsArmCollidingWithShip;
    }

    public static void setIsArmCollidingWithShip(bool colliding)
    {
        mIsArmCollidingWithShip = colliding;
    }

    public static bool isColliderCollidingWithShip()
    {
        return mIsColliderCollidingWithShip;
    }

    public static void setIsColliderCollidingWithShip(bool colliding)
    {
        mIsColliderCollidingWithShip = colliding;
    }

    public static float lastAxisValue()
    {
        return mLastAxisValue;
    }
}
