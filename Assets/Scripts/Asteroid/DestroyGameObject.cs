using UnityEngine;
using System.Collections;

public class DestroyGameObject : MonoBehaviour 
{
	public float timeToLive = 2;
	
	void Start () 
	{
		Object.Destroy (gameObject, timeToLive);
	}
}