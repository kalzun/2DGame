using UnityEngine;
using System.Collections;

public class AsteroidStone : MonoBehaviour 
{
	public float maxStartingVelocity;
	public Rigidbody2D asteroidPrefabNextSizeDown;
	public float newAsteroidVelocity;
	public float asteroidExitAngle;
	public float newAsteroidStartingDistance = 1f;
	public int score;
	public AudioSource explosionSound;
	public GameObject explosionPrefab;


	private AsteroidScore asteroidScore;
	private AsteroidGameManager gm;

	// Use this for initialization
	void Start () 
	{
		asteroidScore = GameObject.FindGameObjectWithTag ("AsteroidScore").GetComponent<AsteroidScore> ();
		gm = GameObject.FindGameObjectWithTag ("AsteroidGameManager").GetComponent<AsteroidGameManager> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Bullet") 
		{
			// Destroy this asteroid
			Object.Destroy (gameObject);
			Object.Destroy (other.gameObject);

			Instantiate (explosionSound);
			Instantiate (explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);

			// If we are bigger than size 1, create two new asteroids (split in half)

			if (asteroidPrefabNextSizeDown)
			{
				// Double up the number of Asteroids 
				gm.AddingNumberOfAsteroidCount();
				Debug.Log ("Asteroidcount should be ADDED");

				// Else, split in half and add som velocity to the new asteroids
				// Velocity should probably be based on the velocity of the bullet
				Vector3 instance1Direction = Quaternion.AngleAxis (asteroidExitAngle, Vector3.forward) * other.relativeVelocity.normalized;

				Vector3 instance2Direction = Quaternion.AngleAxis (-asteroidExitAngle, Vector3.forward) * other.relativeVelocity.normalized;

				Vector3 a1StartingPosition = transform.position + instance1Direction * newAsteroidStartingDistance;
				Vector3 a2StartingPosition = transform.position + instance2Direction * newAsteroidStartingDistance;

				Rigidbody2D asteroidInstance1 = (Rigidbody2D) Instantiate(asteroidPrefabNextSizeDown, a1StartingPosition, transform.rotation);
				Rigidbody2D asteroidInstance2 = (Rigidbody2D) Instantiate(asteroidPrefabNextSizeDown, a2StartingPosition, transform.rotation);

				asteroidInstance1.velocity = instance1Direction * newAsteroidVelocity;
				asteroidInstance2.velocity = instance2Direction * newAsteroidVelocity;
			} else 
			{	// Reduce number of asteroidscount

				gm.ReduceNumberOfAsteroidCount();
			}

			// Score a point for each hit
			asteroidScore.Addscore(score);

	

		}
	}
}
