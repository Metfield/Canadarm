using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpaceShuttle : NetworkBehaviour
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
    protected float throttle;

    protected Vector3 acceleration;
    protected Vector3 rotation;

    protected Vector3 hardcodeZero;
    protected bool axisStarted;

	protected Camera camera;

    // Use this for initialization
    void Start ()
    {
        acceleration = new Vector3(0.0f, 0.0f, 0.0f);
        rotation = new Vector3(0.0f, 0.0f, 0.0f);

        axisStarted = false;

        // Set all axes to 0
        Input.ResetInputAxes();
		camera = GetComponentInChildren<Camera>();

		if (!isLocalPlayer) {
			camera.enabled = false;

		} else
		{
			camera.enabled = true;
		}

    }
	
	// Update is called once per frame
	void Update ()
    {
		if (!isLocalPlayer)
		{
			return;
		}
	
		


        roll = Input.GetAxis("Horizontal");
        pitch = Input.GetAxis("Vertical");
        yaw = Input.GetAxis("RZ Axis");

        throttle = Input.GetAxis("Z Axis");
        
        if (Input.GetKey(KeyCode.Space))
        {            
            acceleration = transform.forward * maxSpeed;
            rigidBody.AddForce(acceleration);
        }
        else
        {
            // This fixes stupid bug when ship starts game by throttling forward
            if (throttle != 0)
                axisStarted = true;

            // Use Joystick
            if (axisStarted)
            {
                acceleration = (throttle + 1.0f) * transform.forward * maxSpeed;
                rigidBody.AddForce(acceleration);
            }
        }

        rigidBody.AddForce(Input.GetAxis("Slider Axis") * transform.right * (maxSpeed * 0.5f));

        // Extra yaw skit
        if (Input.GetKey(KeyCode.E))
        {
            yaw = 1;
        }
        else if(Input.GetKeyUp(KeyCode.E))
        {
            yaw = 0;
        }

        if(Input.GetKey(KeyCode.Q))
        {
            yaw = -1;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            yaw = 0;
        }

        rotation.y = yaw * yawSpeed;
        rotation.x = pitch * pitchSpeed;
        rotation.z = -roll * rollSpeed;        
        
        rigidBody.transform.Rotate(rotation);
        
        // @TODO: ADD A CONDITION IN CASE THERE WAS A COLLISION
        rigidBody.angularVelocity = Vector3.zero;
    }
}
