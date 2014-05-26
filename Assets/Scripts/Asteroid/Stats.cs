using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour 
{
	public TextMesh numberOfAsteroidsMesh;
	public TextMesh playerHitPoints;
	public TextMesh bulletCounter;
	public TextMesh playerLevel;
	public TextMesh versionBuildMesh;
	public float versionBuild;

	private AsteroidGameManager gm;
	private AsteroidPlayerShip player;

	// Use this for initialization
	void Start () 
	{
		gm = GameObject.FindGameObjectWithTag ("AsteroidGameManager").GetComponent<AsteroidGameManager> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<AsteroidPlayerShip> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
	//	Debug.Log ("gm.Number of Asteroids is: " + gm.numberOfAsteroids + " and numberOfAsteroidsMesh is: " + numberOfAsteroidsMesh);
		numberOfAsteroidsMesh.text = "Number of asteroids: " + gm.testCounter;
		playerHitPoints.text = "Hit Points: " + player.hitPoints;
		bulletCounter.text = "Bulletcounter: " + player.bulletCounter;	
		playerLevel.text = "Player level: " + gm.playerLevel;
		versionBuildMesh.text = "versionBuild: " + versionBuild;
	}
}
