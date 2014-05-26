using UnityEngine;
using System.Collections;

public class SelfDesctruct : MonoBehaviour 
{

	public float time = 3f;

	// Use this for initialization
	void Start () {
	
		Invoke ("DestroySelf", time);
	}
	
	void DestroySelf()
	{
  		Destroy (gameObject);
	}
}
