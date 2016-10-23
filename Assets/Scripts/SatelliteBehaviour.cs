using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SatelliteBehaviour : MonoBehaviour
{

    private Vector3 acceleration = new Vector3(0, 0, 0);

    [SerializeField]
    private Vector3 rotation = new Vector3(0, 0, 0);

    [SerializeField]
    private Rigidbody rigidBody;

    [SerializeField]
    private int rotationMultiplier;

    private Vector3 translation;

	private IEnumerator waitAndHandleCollision;

    void OnEnable()
    {
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("CanadarmTip"))
        {
			waitAndHandleCollision = WaitAndHandleCollision ();
			StartCoroutine(waitAndHandleCollision);
           
        }
    }

    public void SetAcceleration(Vector3 acc)
    {
        acceleration = acc;
    }

    // Update is called once per frame
    void Update()
    {
        //not currently working
        this.rigidBody.AddForce(acceleration);
        //this.transform.RotateAround(this.transform.position, Vector3.right, 50 * Time.deltaTime);
        //this.transform.Rotate(Vector3.up * 50 * Time.deltaTime);
        this.transform.Rotate(rotation * rotationMultiplier * Time.deltaTime);
    }

	IEnumerator WaitAndHandleCollision() {
		print(Time.time);
		yield return new WaitForSeconds(1);
		print(Time.time);
		gameObject.SetActive(false);
		//SpawnManager.instance.ReSpawnObject();
		GameManager.instance.IncrementScore();
	}

}