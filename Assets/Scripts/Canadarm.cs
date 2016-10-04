using UnityEngine;
using System.Collections;

public class Canadarm : MonoBehaviour
{
    [SerializeField]
    protected GameObject middlePivot;

    [SerializeField]
    protected float rotationSpeed;

    protected float x, y, z,
                    dx, dy, dz, twist;

    protected Vector3 rotation;

	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Get Joystick values
        dx = Input.GetAxis("Vertical") * rotationSpeed;
        dy = Input.GetAxis("Horizontal") * rotationSpeed;
        dz = Input.GetAxis("Z Axis") * rotationSpeed;

        twist = Input.GetAxis("RZ Axis") * rotationSpeed;

        rotation.x = -dx;
        rotation.y = dy;
        rotation.z = -twist;

        this.transform.Rotate(rotation);

        // -dz in X
        middlePivot.transform.Rotate(new Vector3(-dz, 0.0f, 0.0f));        
    }
}
