using UnityEngine;
using System.Collections;

public class Canadarm : MonoBehaviour
{
    [SerializeField]
    protected GameObject upperArm;

    [SerializeField]
    protected GameObject foreArm;

    [SerializeField]
    protected float rotationSpeed;

    [SerializeField]
    protected float foreArmMaxAngle;

    /*[SerializeField]
    protected GameObject spaceShuttle;*/

    protected float x, y, z,
                    dx, dy, dz, twist;

    protected Vector3 rotation;

    protected float xRotationHack;

    // Rigid bodies
    protected Rigidbody upperArmRigidBody;
    protected Rigidbody foreArmRigidBody;

    // Use this for initialization
    void Start()
    {
        // Get rigid bodies
        upperArmRigidBody = upperArm.GetComponent<Rigidbody>();
        foreArmRigidBody = foreArm.GetComponent<Rigidbody>();

        // Initialize hack
        xRotationHack = 0.0f;

        // Init rotation vector
        rotation = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        // Add Space shuttle's transformations
       /* transform.Rotate(spaceShuttle.transform.rotation.eulerAngles);*/



        twist = Input.GetAxis("RZ Axis") * rotationSpeed;

        // Do Upper arm movement
        dx = Input.GetAxis("Vertical") * rotationSpeed;
        dy = Input.GetAxis("Horizontal") * rotationSpeed;
        
        upperArmRigidBody.AddForce(dx * transform.up);
        upperArmRigidBody.AddForce(dy * transform.right);

        // Get forearm movement rotation factor
        dz = Input.GetAxis("Z Axis") * rotationSpeed * 0.01f;
        xRotationHack += dz;

        // Check for max angle boundaries
        if (Mathf.Abs(xRotationHack) < foreArmMaxAngle)
        {
            // Rotate forearm 
            rotation.x = -dz;
            foreArmRigidBody.transform.Rotate(rotation);
        }
        else
        {
            // If it's bigger then push it back to the boundaries
            xRotationHack -= dz;
        }
    }

}
