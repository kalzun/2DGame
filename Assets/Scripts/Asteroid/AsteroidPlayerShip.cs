using UnityEngine;
using System.Collections;

public class AsteroidPlayerShip : MonoBehaviour 
{
	public float rotationPower;
	public float thrustPower;
	public float maxRotationSpeed;
	public float maxTravelSpeed;
	public int hitPoints = 3;
	public AudioSource hurtSound;




	public bool allowBackwardsThrust = true;

	public Rigidbody2D bulletPrefab;
	public float bulletSpeed = 100;
	public int bulletCounter = 0;

	private AsteroidGameManager asteroidGameManager;
	public GameObject mainCamera;


	private bool isDead = false;
	private bool playerHit = false;
	

	void Start()
	{
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		asteroidGameManager = GameObject.FindGameObjectWithTag ("AsteroidGameManager").GetComponent<AsteroidGameManager> ();
	}


	void Update()
	{
		// Shoot bullet!
		if (!isDead && !playerHit) 
		{
			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				ShootBullet();
				/*if (gotBFG9000)
				{
					Invoke ("ShootBullet", 0.3f);
					Invoke ("ShootBullet", 0,6f);
				}*/
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		if (!isDead && !playerHit) 
		{
		// Rotation
			if (rigidbody2D.angularVelocity < maxRotationSpeed && Input.GetAxis ("Horizontal") < 0) 
			{
				rigidbody2D.AddTorque (-Input.GetAxis ("Horizontal") * rotationPower);
			}
			if (rigidbody2D.angularVelocity > -maxRotationSpeed && Input.GetAxis ("Horizontal") > 0) 
			{
				rigidbody2D.AddTorque (-Input.GetAxis ("Horizontal") * rotationPower);
			}

			// If rotation speed is higher than what we have defined maxRotationSpeed to be
			if (rigidbody2D.angularVelocity > maxRotationSpeed) 
			{ // ... then set the current rotation (angularVelocity) to be the same as maxRotatonSpeed
				rigidbody2D.angularVelocity = maxRotationSpeed;
			}
			if (rigidbody2D.angularVelocity < -maxRotationSpeed) 
			{
				rigidbody2D.angularVelocity = -maxRotationSpeed;
			}

			// THRUST
			// If the travelling speed (velocity.magnitude) is less than our maxForwardSpeed
			if (rigidbody2D.velocity.magnitude <= maxTravelSpeed) 
			{ // ... then allow for more thrust to be applied
				if (allowBackwardsThrust) 
				{
					rigidbody2D.AddForce (transform.up * thrustPower * Input.GetAxis ("Vertical"));			
				} 
				else 
				{
					rigidbody2D.AddForce (transform.up * thrustPower * Mathf.Max (Input.GetAxis ("Vertical"), 0));	
				}
			}

			// Cap velocity at maxTravelSpeed
			if (rigidbody2D.velocity.magnitude > maxTravelSpeed) { // ... then set it to be what we have defined as our maxForwardSpeed
					rigidbody2D.velocity = rigidbody2D.velocity.normalized * maxTravelSpeed;
			}
		}
	}
	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.CompareTag ("Asteroid")) 
		{
			iTween.ShakePosition (mainCamera, iTween.Hash ("amount", new Vector3 (0.1f, 0.1f, 0f), "time", 0.5f));
			Instantiate (hurtSound);
			playerHit = true;
			hitPoints = hitPoints - 1;
			Debug.Log ("Hit asteroid, updated HITPOINTS: " + hitPoints);
			if (hitPoints <= 0 && playerHit)
			{
				isDead = true;
			}

			if (!isDead && playerHit)
			{
				InvincibleMode();
			}

			if (isDead && playerHit)
			{
				AsteroidScore score = GameObject.FindGameObjectWithTag ("AsteroidScore").GetComponent<AsteroidScore>();
				
				// if (score)
				// {
					score.UpdateHighScoreOnDeath ();
				// }

				asteroidGameManager.PlayerDied (score);
			}


		}
	}
	void InvincibleMode()
	{ 
		{
			Debug.Log ("Invincible mode enabled");
			gameObject.collider2D.enabled = false;
			Invoke ("ColliderOnAgain", 2);

		}
	}

	void ShootBullet()
	{
		Rigidbody2D bulletInstance = (Rigidbody2D)Instantiate (bulletPrefab, transform.position, transform.rotation);
		bulletInstance.velocity = transform.rotation * Vector2.right * bulletSpeed;
		bulletCounter++;
	}

	void ColliderOnAgain()
	{
		gameObject.collider2D.enabled = true;
		playerHit = false;
		return;
	}


}

	
