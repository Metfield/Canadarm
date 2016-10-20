using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpaceShuttle : NetworkBehaviour
{
    [SerializeField]
    protected Rigidbody rigidBody;
	protected Rigidbody shuttleRigidBody;

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

	protected Camera cameraToMove;
	protected Camera cameraNotToMove;

	protected GameObject shuttleObject;

    // Use this for initialization
    void Start ()
    {
		
		shuttleObject = GameObject.FindGameObjectWithTag ("SpaceShuttle");


		shuttleRigidBody = shuttleObject.GetComponent<Rigidbody> ();

        acceleration = new Vector3(0.0f, 0.0f, 0.0f);
        rotation = new Vector3(0.0f, 0.0f, 0.0f);

        axisStarted = false;

        // Set all axes to 0
        Input.ResetInputAxes();
		cameraToMove = shuttleObject.GetComponentInChildren<Camera>();
		cameraNotToMove = GetComponentInChildren<Camera>();

		Debug.Log ("camera to move: " + cameraToMove.tag);
		Debug.Log ("camera not to move: " + cameraNotToMove.tag);

		cameraNotToMove.enabled = false;
		cameraToMove.enabled = true;

    }

  public float GetMaxSpeed()
  {
    return maxSpeed;
  }

	// Update is called once per frame
	void Update ()
    {
		if (!isLocalPlayer)
		{
			return;
		}
	
		Debug.Log ("Input: " + Input.inputString);

        roll = Input.GetAxis("Horizontal");
        pitch = Input.GetAxis("Vertical");
        yaw = Input.GetAxis("RZ Axis");

        throttle = Input.GetAxis("Z Axis");

        if (Input.GetKey(KeyCode.Space))
        {            
            acceleration = shuttleObject.transform.forward * maxSpeed;
            shuttleRigidBody.AddForce(acceleration);
        }
        else
        {
            // This fixes stupid bug when ship starts game by throttling forward
            if (throttle != 0)
                axisStarted = true;

            // Use Joystick
            if (axisStarted)
            {
				acceleration = (throttle + 1.0f) * shuttleObject.transform.forward * maxSpeed;
                shuttleRigidBody.AddForce(acceleration);
            }
        }

		shuttleRigidBody.AddForce(Input.GetAxis("Slider Axis") * shuttleObject.transform.right * (maxSpeed * 0.5f));

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
        
        shuttleRigidBody.transform.Rotate(rotation);

        // @TODO: ADD A CONDITION IN CASE THERE WAS A COLLISION
        shuttleRigidBody.angularVelocity = Vector3.zero;
    }
}
