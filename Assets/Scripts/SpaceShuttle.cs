using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpaceShuttle : NetworkBehaviour
{
	//SpaceShuttle stuff
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

	//Canadarm stuff
	protected GameObject forearm;
	protected GameObject canadarm;
    protected GameObject hackadarm;
    protected GameObject shuttle;


	[SerializeField]
	protected float middlePivotMaxAngle; // 145

	[SerializeField]
	protected float basePivotLowerMaxAngle; //12

	[SerializeField]
	protected float basePivotUpperMaxAngle; //50

	[SerializeField]
	protected float basePivotHorizontalMaxAngle; // 65

	[SerializeField]
	protected float rotationSpeed;
        
    protected GameObject canvas;

	protected float basePivotCurrentVerticalAngle;
	protected float basePivotCurrentHorizontalAngle;

	protected float dx, dy, dz, twist;

	protected Vector3 rotationCanadarmX, rotationCanadarmY;
	protected float middlePivotCurrentAngle;

    

    // Use this for initialization
    void Start ()
    {
        // Gets the space shuttle impostors to FUCK THE FUCK OFF
		if (gameObject.tag != "SpaceShuttle")
        {
            if(GameManager.instance.getMyBalls() == 0)
                gameObject.transform.position = new Vector3(100000, 100000, 100000);                       
            else
                gameObject.transform.position = new Vector3(100000, 100000, -100000);
        }

		shuttleObject = GameObject.FindGameObjectWithTag ("SpaceShuttle");


		shuttleRigidBody = shuttleObject.GetComponent<Rigidbody> ();

        acceleration = new Vector3(0.0f, 0.0f, 0.0f);
        rotation = new Vector3(0.0f, 0.0f, 0.0f);

        axisStarted = false;

        // Set all axes to 0
        Input.ResetInputAxes();
		cameraToMove = shuttleObject.GetComponentInChildren<Camera>();
		cameraNotToMove = GetComponentInChildren<Camera>();

		//Debug.Log ("camera to move: " + cameraToMove.tag);
		//Debug.Log ("camera not to move: " + cameraNotToMove.tag);

		cameraNotToMove.enabled = false;
		cameraToMove.enabled = true;

		//Canadarm stuff
		canadarm = shuttleObject.transform.Find("Cupola/Canadarm").gameObject;
        hackadarm = shuttleObject.transform.Find("Cupola/Canadarm/HackadarmRotation").gameObject;
		forearm = shuttleObject.transform.Find("Cupola/Canadarm/HackadarmRotation/Base Pivot/UpperArm/Forearm").gameObject;

        // Get Canvas stuff
        canvas = GameObject.Find("Canvas");

		/*if (canadarm) {
			Debug.Log ("chinpokomon");
		}
		else {
			Debug.Log ("not chinpokomon");
		}
		if (forearm) {
			Debug.Log ("alabama man");
		}
		else {
			Debug.Log ("not alabama man");
		}*/

		middlePivotCurrentAngle = 0.0f;
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
	
		//Debug.Log ("Input: " + Input.inputString);

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

        /*if (shuttleRigidBody) {
			Debug.Log ("shuttlerigidbody");
		}
		else {
			Debug.Log ("not shuttlerigidbody");
		}
		if (shuttleObject) {
			Debug.Log ("shuttle object");
		}
		else {
			Debug.Log ("not shuttle object");
		}*/

     
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

        // Update canvas' position
        canvas.transform.rotation = shuttleObject.transform.rotation;
        canvas.transform.position = shuttleObject.transform.position;

        UpdateCanadarm ();
    }

    public override void OnDeserialize(NetworkReader reader, bool initialState)
    {
        base.OnDeserialize(reader, initialState);
    }

    public override bool OnSerialize(NetworkWriter writer, bool initialState)
    {
        return base.OnSerialize(writer, initialState);
    }

    void UpdateCanadarm()
    {
		//Debug.Log ("sug kuk space shuttle");
		// Get Joystick values
		dx = Input.GetAxis("Vertical2") * rotationSpeed;
		dy = Input.GetAxis("Horizontal2") * rotationSpeed;
		dz = Input.GetAxis("Z Axis2") * rotationSpeed;

		// Update arm's custom rotation values
		basePivotCurrentVerticalAngle -= dx;
		basePivotCurrentHorizontalAngle += dy;
		middlePivotCurrentAngle -= dz;

		// Set rotation to zero
		rotationCanadarmX = Vector3.zero;
        rotationCanadarmY = Vector3.zero;

        twist = Input.GetAxis("RZ Axis") * rotationSpeed;

		// Check for horizontal rotation boundaries at base
		if(Mathf.Abs(basePivotCurrentHorizontalAngle) < basePivotHorizontalMaxAngle)
		{
			// Rotation is allowed
			rotationCanadarmY.y = dy;
		}
		else
		{
			basePivotCurrentHorizontalAngle -= dy;
		}

		// Now check both vertical boundaries
		if ( (basePivotCurrentVerticalAngle < basePivotUpperMaxAngle) &&
			(basePivotCurrentVerticalAngle > basePivotLowerMaxAngle) )
		{
			// Rotation is allowed
			rotationCanadarmX.x = -dx;
		}
		else
		{
			basePivotCurrentVerticalAngle += dx;
		}

        // Hackadarm hack
        canadarm.transform.Rotate(new Vector3(rotationCanadarmX.x, 0, 0), Space.Self);
        hackadarm.transform.Rotate(new Vector3(0, rotationCanadarmY.y, 0), Space.Self);

        //hackadarm.transform.Rotate(new Vector3(0.0f, 0.0f, -twist), Space.Self);

        // Check for max angle
        if (Mathf.Abs(middlePivotCurrentAngle) < middlePivotMaxAngle)
		{
			// Pivot is allowed to rotate
			forearm.transform.Rotate(new Vector3(-dz, 0.0f, 0.0f));
		}
		else
		{
			// Out of boundaries, fix boundary
			middlePivotCurrentAngle += dz;
		}
	}
}
