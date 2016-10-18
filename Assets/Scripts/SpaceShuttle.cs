using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpaceShuttle : NetworkBehaviour
{
	protected Rigidbody playerRigidBody;

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

	protected GameObject shuttle;
	protected GameObject objectToMove;

    // Use this for initialization
    void Start ()
    {
		
		//shuttle = GameObject.FindWithTag ("Player");
		objectToMove = GameObject.FindGameObjectWithTag ("Player");

        playerRigidBody = objectToMove.GetComponent<Rigidbody>();//      objectToMove.GetComponent<Rigidbody> ();

        acceleration = new Vector3(0.0f, 0.0f, 0.0f);
        rotation = new Vector3(0.0f, 0.0f, 0.0f);

        axisStarted = false;

        // Set all axes to 0
        Input.ResetInputAxes();
		cameraToMove = objectToMove.GetComponentInChildren<Camera>();
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


        Debug.Log("Suck mah balls: " + roll);

        throttle = Input.GetAxis("Z Axis");

        if (Input.GetKey(KeyCode.Space))
        {            
            acceleration = objectToMove.transform.forward * maxSpeed;
            playerRigidBody.AddForce(acceleration);
        }
        else
        {
            // This fixes stupid bug when ship starts game by throttling forward
            if (throttle != 0)
                axisStarted = true;

            // Use Joystick
            if (axisStarted)
            {
				acceleration = (throttle + 1.0f) * objectToMove.transform.forward * maxSpeed;
                playerRigidBody.AddForce(acceleration);
            }
        }

		playerRigidBody.AddForce(Input.GetAxis("Slider Axis") * objectToMove.transform.right * (maxSpeed * 0.5f));

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
        
        playerRigidBody.transform.Rotate(rotation, Space.Self);

        // @TODO: ADD A CONDITION IN CASE THERE WAS A COLLISION
        playerRigidBody.angularVelocity = Vector3.zero;
    }
}
