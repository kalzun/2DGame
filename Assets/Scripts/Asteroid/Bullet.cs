using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	public float timeToLive = 2;
	
	void Start () 
	{
		Object.Destroy (gameObject, timeToLive);
	}
}