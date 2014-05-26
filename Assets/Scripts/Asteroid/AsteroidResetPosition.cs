using UnityEngine;
using System.Collections;

public class AsteroidResetPosition : MonoBehaviour 
{
	// public Vector2 stageArea = new Vector2 (4.1f, 3.3f); 

	public bool wraparoundActive = true;

	public AsteroidGameManager gm;

	void Start()
	{
		gm = GameObject.FindGameObjectWithTag ("AsteroidGameManager").GetComponent<AsteroidGameManager> ();
			
	}

	void Update()
	{
		if (wraparoundActive) {
			ResetPositionOnWraparound ();
		} 
		else 
		{	// We want to set wraparound to active if the object has entered the stage area
			if (IsObjectInStageArea())
			{ // We have entered the stage area, so enable wraparound like normal
				wraparoundActive = true;
			}
		}
	}

	bool IsObjectInStageArea()
	{
		if (transform.position.x < -gm.stageArea.x || transform.position.x > gm.stageArea.x) 
		{	// Object is outside stage area
			return false;
		}

		if (transform.position.y < -gm.stageArea.y || transform.position.y > gm.stageArea.y) 
		{	// Object is outside stage area
			return false;
		}
		return true;
	}

	// This function changes the object's position if it leaves the screen to make it enter
	// on the other side
	void ResetPositionOnWraparound()
	{
		if (transform.position.x > gm.stageArea.x)
		{
			transform.position = new Vector2(-gm.stageArea.x, transform.position.y);
		}
		if (transform.position.y > gm.stageArea.y) 
		{
			transform.position = new Vector2(transform.position.x, -gm.stageArea.y);
		}
		
		if (transform.position.x < -gm.stageArea.x) 
		{
			transform.position = new Vector2(gm.stageArea.x, transform.position.y);
		}
		if (transform.position.y < -gm.stageArea.y) 
		{
			transform.position = new Vector2(transform.position.x, gm.stageArea.y);
		}
	}
}
