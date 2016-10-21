using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Canadarm : NetworkBehaviour
{
    [SerializeField]
    protected GameObject middlePivot;

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

    protected float basePivotCurrentVerticalAngle;
    protected float basePivotCurrentHorizontalAngle;

    protected float dx, dy, dz, twist;

    protected Vector3 rotation;
    protected float middlePivotCurrentAngle;

    [SyncVar]
	protected GameObject forearm;
    [SyncVar]
    protected GameObject canadarm;
    [SyncVar]
    protected GameObject shuttle;
    
    // Use this for initialization
    void Start()
    {
		shuttle = GameObject.FindGameObjectWithTag ("SpaceShuttle");
		canadarm = shuttle.transform.Find("Cupola/Canadarm").gameObject;
		forearm = shuttle.transform.Find("Cupola/Canadarm/Base Pivot/UpperArm/Forearm").gameObject;

        middlePivotCurrentAngle = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Get Joystick values
        dx = Input.GetAxis("Vertical") * rotationSpeed;
        dy = Input.GetAxis("Horizontal") * rotationSpeed;
        dz = Input.GetAxis("Z Axis") * rotationSpeed;

        // Update arm's custom rotation values
        basePivotCurrentVerticalAngle -= dx;
        basePivotCurrentHorizontalAngle += dy;
        middlePivotCurrentAngle -= dz;

        // Set rotation to zero
        rotation = Vector3.zero;

        twist = Input.GetAxis("RZ Axis") * rotationSpeed;

        // Check for horizontal rotation boundaries at base
        if(Mathf.Abs(basePivotCurrentHorizontalAngle) < basePivotHorizontalMaxAngle)
        {
            // Rotation is allowed
            rotation.y = dy;
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
            rotation.x = -dx;
        }
        else
        {
            basePivotCurrentVerticalAngle += dx;
        }

        canadarm.transform.localRotation = Quaternion.Euler(basePivotCurrentVerticalAngle, basePivotCurrentHorizontalAngle, 0.0f);
 
        // Check for max angle
        if (Mathf.Abs(middlePivotCurrentAngle) < middlePivotMaxAngle)
        {
            // Pivot is allowed to rotate
            forearm.transform.localRotation = Quaternion.Euler(middlePivotCurrentAngle + 90, 0.0f, 0.0f);       /* new Vector3(-middlePivotCurrentAngle, 0.0f, 0.0f));*/
        }
        else
        {
            // Out of boundaries, fix boundary
            middlePivotCurrentAngle += dz;
        }
    }
}
