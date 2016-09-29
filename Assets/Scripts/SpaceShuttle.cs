using UnityEngine;
using System.Collections;

public class SpaceShuttle : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody rigidBody;

    [SerializeField]
    protected float maxSpeed;

    [SerializeField]
    protected float rollSpeed;

    [SerializeField]
    protected float yawSpeed;

    [SerializeField]
    protected float pitchSpeed;

    protected float roll, pitch, yaw;

    protected Vector3 acceleration;
    protected Vector3 rotation;

    protected Vector3 hardcodeZero;

	// Use this for initialization
	void Start ()
    {
        acceleration = new Vector3(0.0f, 0.0f, 0.0f);
        rotation = new Vector3(0.0f, 0.0f, 0.0f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        roll = Input.GetAxis("Horizontal");
        pitch = Input.GetAxis("Vertical");        

        if(Input.GetKey(KeyCode.Space))
        {            
            acceleration = transform.forward * maxSpeed;
            rigidBody.AddForce(acceleration);
        }

        yaw = 0;

        // Extra yaw skit
        if(Input.GetKey(KeyCode.E))
        {
            yaw = 1;
        }

        if(Input.GetKey(KeyCode.Q))
        {
            yaw = -1;
        }

        rotation.x = pitch * pitchSpeed;
        rotation.z = -roll * rollSpeed;
        rotation.y = yaw * yawSpeed;
        
        rigidBody.transform.Rotate(rotation);
        
        // @TODO: ADD A CONDITION IN CASE THERE WAS A COLLISION
        rigidBody.angularVelocity = Vector3.zero;
    }
}
