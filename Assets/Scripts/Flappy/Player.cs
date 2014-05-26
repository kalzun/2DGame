using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{

	public float myRotationSpeed = 100.0f;
	public float myWalkSpeed = 10;
	public float myJumpForce = 3;

	// Use this for initialization
	void Start () 
	{
		//transform.position = new Vector3 (0, 3, 0);
	}

	void Update () 
	{
		if (Input.GetKey (KeyCode.LeftArrow))
		{
			transform.Rotate (Vector3.up, -myRotationSpeed * Time.deltaTime);
		}

		if (Input.GetKey (KeyCode.RightArrow)) 
		{
			transform.Rotate (Vector3.up, myRotationSpeed * Time.deltaTime);
		}

		if (Input.GetKey (KeyCode.UpArrow)) 
		{
			transform.Translate (Vector3.forward * myWalkSpeed * Time.deltaTime);
		}

		if (Input.GetKey (KeyCode.DownArrow))
		{
			transform.Translate (Vector3.back * myWalkSpeed * Time.deltaTime);
		}

		if (Input.GetKeyDown (KeyCode.Space))
		{
			rigidbody.velocity = Vector3.up * myJumpForce;
		}
		//
	}
}
