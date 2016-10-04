using UnityEngine;
using System.Collections;

public class Canadarm : MonoBehaviour
{
    [SerializeField]
    protected GameObject pivotOne;

    [SerializeField]
    protected GameObject pivotTwo;

    protected float x, y, z,
                    dx, dy, dz;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Get Joystick values
        dx = Input.GetAxis("Horizontal");
        dy = Input.GetAxis("Vertical");
        dz = Input.GetAxis("Z Axis");

        

    }
}
