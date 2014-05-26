using UnityEngine;
using System.Collections;

public class ArtifactsOptions : MonoBehaviour 	
{
	public float maxStartingVelocity;
	public float spawnVelocity = 2f;
	public float newAsteroidVelocity;
	public float asteroidExitAngle;
	public int score;

	public float newAsteroidStartingDistance = 1f;
	// public AudioSource pickupSound;
	// public AudioSource destroySound;

	// public Rigidbody2D heartGemPrefab;

	private AsteroidGameManager gm;
	private AsteroidPlayerShip player;
	private AsteroidScore asteroidScore;



	
	// Use this for initialization
	void Start () 
	{
		Debug.Log ("Artifacts.cs loaded");
		gm = GameObject.FindGameObjectWithTag ("AsteroidGameManager").GetComponent<AsteroidGameManager> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<AsteroidPlayerShip> ();
		asteroidScore = GameObject.FindGameObjectWithTag ("AsteroidScore").GetComponent<AsteroidScore> ();
		// stats = GameObject.FindGameObjectWithTag ("AsteroidGameManager").GetComponent<Stats> ();

	}

	void Update()
	{

	}
	
	void OnTriggerEnter2D (Collider2D other)
	{


		if (other.gameObject.CompareTag ("Bullet")) 
		{

			// Destroy this artifact
			gm.ReduceNumberOfArtifactCount();
			Object.Destroy (gameObject);
			Object.Destroy (other.gameObject);


			
			// audio.Play();
			
			// Instantiate (explosionSound);
			
			// If we are bigger than size 1, create two new asteroids (split in half)

		}
		if (other.gameObject.CompareTag ("Player")) 
		{
			gm.ReduceNumberOfArtifactCount();
			// Add 1 to players Hit Points
			player.hitPoints++;
			// Add score for picking up artifact
			asteroidScore.Addscore (score);

			Object.Destroy (gameObject);

		}
	}


}
