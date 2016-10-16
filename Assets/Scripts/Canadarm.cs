using UnityEngine;
using System.Collections;

public class Canadarm : MonoBehaviour
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
    
    // Use this for initialization
    void Start()
    {
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


        this.transform.Rotate(rotation, Space.World);
        //this.transform.Rotate(new Vector3(0.0f, 0.0f, -twist), Space.Self);

        // Check for max angle
        if (Mathf.Abs(middlePivotCurrentAngle) < middlePivotMaxAngle)
        {
            // Pivot is allowed to rotate
            middlePivot.transform.Rotate(new Vector3(-dz, 0.0f, 0.0f));
        }
        else
        {
            // Out of boundaries, fix boundary
            middlePivotCurrentAngle += dz;
        }
    }
}
