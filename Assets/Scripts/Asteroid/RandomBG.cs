using UnityEngine;
using System.Collections;

public class RandomBG : MonoBehaviour 
{
	private Sprite[] backgrounds;

	void Awake ()
	{
		backgrounds = Resources.LoadAll<Sprite> ("backgrounds");
	}

	// Use this for initialization
	void Start () 
	{

		int index = Random.Range (0, (backgrounds.Length));

		GameObject background = new GameObject ();
		background.AddComponent<SpriteRenderer> ();
		background.GetComponent<SpriteRenderer> ().sprite = backgrounds [index];

		// Place background behind everything
		Vector3 positioning = new Vector3 (0, 0, 5);
		background.transform.position = positioning;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
