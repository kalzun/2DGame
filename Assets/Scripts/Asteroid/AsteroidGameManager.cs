using UnityEngine;
using System.Collections;

public class AsteroidGameManager : MonoBehaviour 
{
	public Vector2 stageArea = new Vector2 (4.3f, 3.3f); 


	public int startingAsteroids = 5;
	public float spawnFrequency = 10f;
	public float spawnNumber = 1f;
	public float spawnVelocity = 2f;
	public float spawnFreqIncrease = 0.1f;
	public float spawnNmbrIncrease = 1;

	public AudioSource gameOverSound;

	public Rigidbody2D asteroidPrefab;
	public Rigidbody2D heartGemPrefab;
	public TextMesh gameOverText;

	// public int numberOfAsteroids;

	public int testCounter;

	public int maxLargeAsteroids;
	public int numberOfArtifacts;
	public int maxArtifacts; 
	public AsteroidScore asteroidScore;
	private AsteroidPlayerShip player;

	public int scoreToLevelAdjust;
	public int spawnIncreaseByLevel;
	public int playerLevel;

	private SpriteRenderer startscreen;
	public GameObject startGameObject;
	// private bool firstGame;
	static int gameCounter;

	// Use this for initialization
	void Start () 
	{
		startscreen = GameObject.Find ("startscreen").GetComponent<SpriteRenderer> ();
		startGameObject = GameObject.Find ("startscreen");
			// Spawn "i" - number of asteroids at the start of game.
		for (int i = 0 ; i < startingAsteroids ; ++i)
		{
			SpawnAsteroid();
			SpawnArtifact();
		}
		asteroidScore = GameObject.FindGameObjectWithTag ("AsteroidScore").GetComponent<AsteroidScore> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<AsteroidPlayerShip> ();
	}

	// Update is called once per frame
	void Update () 
	{
		// Check if first time playing
		if (gameCounter == 0) 
		{
			Time.timeScale = 0;
			Startinfo ();
		}

		InvokeRepeating ("SpawnAsteroid", spawnFrequency, spawnNumber);	 

		if (player.hitPoints == 1) 
		{
			SpawnArtifact();	
			return;
		}

		InvokeRepeating ("SpawnArtifact", 60, 30);

		if (asteroidScore.score == scoreToLevelAdjust) 
		{
			spawnFrequency = spawnFrequency - spawnFreqIncrease;
			spawnNumber = spawnNumber - spawnNmbrIncrease;
			maxLargeAsteroids = maxLargeAsteroids + maxLargeAsteroids/2;
			Debug.Log ("New Spawnfrequency " + spawnFrequency + " and new Spawnfrequency " + spawnNumber + " and new MaxLargeAsteroids" + maxLargeAsteroids);
			scoreToLevelAdjust = scoreToLevelAdjust + scoreToLevelAdjust;
			playerLevel = playerLevel + 1;
			Debug.Log ("ScoreToLevelAdjust increased to " + scoreToLevelAdjust);
			Debug.Log ("Player level " + playerLevel);
		}
	}

	void SpawnAsteroid()
	{
		 if (testCounter >= maxLargeAsteroids) 
		{
			return;
			Debug.Log("testCounter is bigger than or same as maxLargeAsteroids");
		}


		// Add number of Asteroids
		// numberOfAsteroids = numberOfAsteroids++;
		testCounter++;
		 Debug.Log ("NumberOfAsteroids: " + testCounter);

		float spawnPosX = Random.Range (-stageArea.x, stageArea.x);

		if (spawnPosX < 0) 
		{
			spawnPosX -= stageArea.x;
		}
		if (spawnPosX > 0) 
		{
			spawnPosX += stageArea.x;
		}

		if (spawnPosX > 0) 
		{
			spawnPosX += stageArea.x;		
		}
		float spawnPosY = Random.Range (-stageArea.y, stageArea.y);
		if (spawnPosY > 0) 
		{
			spawnPosY += stageArea.y;		
		}

		Vector2 spawnPos = new Vector2 (spawnPosX, spawnPosY);
		Vector2 targetPos = new Vector2(Random.Range (0, stageArea.x), Random.Range(0, stageArea.y));

		Rigidbody2D asteroid = (Rigidbody2D) Instantiate(asteroidPrefab, spawnPos, transform.rotation);
		asteroid.velocity = (targetPos - spawnPos).normalized * spawnVelocity;

		AsteroidResetPosition arp = asteroid.GetComponent<AsteroidResetPosition> ();
		arp.wraparoundActive = false; // No wrapping until we have entered the stage area

	}

	public void SpawnArtifact()
	{
		if (numberOfArtifacts >= maxArtifacts) 
		{
			return;
		}

		AddingNumberOfArtifactCount ();

		float spawnPosX = Random.Range (-stageArea.x, stageArea.x);
		
		if (spawnPosX < 0) 
		{
			spawnPosX -= stageArea.x;
		}
		if (spawnPosX > 0) 
		{
			spawnPosX += stageArea.x;
		}
		
		if (spawnPosX > 0) 
		{
			spawnPosX += stageArea.x;		
		}
		float spawnPosY = Random.Range (-stageArea.y, stageArea.y);
		if (spawnPosY > 0) 
		{
			spawnPosY += stageArea.y;		
		}
		
		Vector2 spawnPos = new Vector2 (spawnPosX, spawnPosY);
		Vector2 targetPos = new Vector2(Random.Range (0, stageArea.x), Random.Range(0, stageArea.y));
		
		Rigidbody2D heartGem = (Rigidbody2D) Instantiate(heartGemPrefab, spawnPos, transform.rotation);
		heartGem.velocity = (targetPos - spawnPos).normalized * spawnVelocity;
		
		AsteroidResetPosition arp = heartGem.GetComponent<AsteroidResetPosition> ();
		arp.wraparoundActive = false; // No wrapping until we have entered the stage area
		
	}

	// ASTEROIDS COUNTING

	public void AddingNumberOfAsteroidCount()
	{
		// numberOfAsteroids = numberOfAsteroids ++;
		testCounter++;
		Debug.Log ("Adding Number of Asteroid " + testCounter);
	}
	
	public void ReduceNumberOfAsteroidCount()
	{
		// numberOfAsteroids = numberOfAsteroids - 1;
		testCounter = testCounter - 1;
	}

	// ARTIFACTS COUNTING

	public void AddingNumberOfArtifactCount()
	{
		numberOfArtifacts = numberOfArtifacts + 1;
		// Debug.Log ("Number of ARTIFACTS increased: " + numberOfArtifacts);
	}
	
	public void ReduceNumberOfArtifactCount()
	{
		numberOfArtifacts = numberOfArtifacts - 1;
		// Debug.Log ("Number of ARTIFACTS decreased: " + numberOfArtifacts);
	}

	public void Startinfo()
	{
		startscreen.transform.position = new Vector3 (0f, -4.31f, 0f);
		startscreen.renderer.enabled = true;
		iTween.MoveTo (startGameObject, iTween.Hash ("position", new Vector3(0, 0, 0f), "time", 1.5f, "ignoretimescale", true));
		

		if (Input.GetKeyDown (KeyCode.Space))
		{
			iTween.FadeTo (startGameObject, iTween.Hash ("alpha", 0, "time", 1f, "onComplete", "DestroyGO", "onCompleteTarget", gameObject));
			// startscreen.renderer.enabled = false;
			Time.timeScale = 1;
			++gameCounter;
		}

	}

	void DestroyGO()
	{
		Destroy (startscreen);
	}

	public void PlayerDied(AsteroidScore score)
	{
		// This simply reloads level 0.
		// TODO Make this a bit more proper.
		Instantiate (gameOverSound);
		Invoke ("ReloadLevel", 3f);
		gameOverText.gameObject.SetActive (true);
		
	}

	void ReloadLevel()
	{
		Application.LoadLevel (0);	
	}
	

}

