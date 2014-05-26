using UnityEngine;
using System.Collections;

public class ObstacleFlappy : MonoBehaviour 
{

	public float verticalMaximumStartPosition;
	public float verticalMinimumStartPosition;
	public float horizontalStartPosition;
	public float horizontalEndPosition;

	Transform cameraPos;
	public float cameraPositionX;


	private GameManager gm;

	void Start () 
	{
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager>();

		Reset (transform.position.x);

	}

	void Update ()
	{
		GameObject getCameraPositionX = GameObject.FindGameObjectWithTag ("MainCamera");
		
		cameraPos = getCameraPositionX.transform;
		cameraPositionX = cameraPos.position.x;
		horizontalEndPosition = cameraPositionX - Random.Range(6, 20);

	}
	

	void FixedUpdate () 
	{
		rigidbody2D.velocity = -Vector2.right * gm.levelSpeed;
		if (transform.position.x < horizontalEndPosition) 
		{
			Reset (horizontalStartPosition);
		}

	}

	void Reset(float horizontalStartPos)
	{
	float randomVerticalStartPosition = Random.Range (verticalMinimumStartPosition, verticalMaximumStartPosition);
		transform.position = new Vector2 (horizontalStartPosition + cameraPositionX, randomVerticalStartPosition);

	}
	
}