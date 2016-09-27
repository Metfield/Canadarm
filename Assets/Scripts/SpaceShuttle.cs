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
    protected float pitchSpeed;

    protected float roll, pitch;

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

        rotation.x = pitch * pitchSpeed;
        rotation.y = roll * rollSpeed;
        
        rigidBody.transform.Rotate(rotation);
    }
}
