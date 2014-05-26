using UnityEngine;
using System.Collections;

public class AsteroidScore : MonoBehaviour 
{

	public TextMesh scoreTextMesh;
	public TextMesh highScoreTextMesh;
	
	public int score = 0;
	public int highScore = 0;
	public float speedMultiplier = 1f;
	
	void Awake()
	{
		scoreTextMesh.text = "Score: 0";
		highScoreTextMesh.text = "High Score: " + PlayerPrefs.GetInt ("AsteroidHighScore", 0);


	}
	

	public void UpdateHighScoreOnDeath()
	{

		int savedHighScore = PlayerPrefs.GetInt ("AsteroidHighScore", 0);
		if (score > savedHighScore) 
		{

			PlayerPrefs.SetInt("AsteroidHighScore", score);		
		}
	}

	public void Addscore(int sc)
	{
		// SCORE IS HIGHER IF YOU HAVE MORE SPEED:
		/* AsteroidPlayerShip player = GameObject.FindGameObjectWithTag ("Player").GetComponent<AsteroidPlayerShip> ();
		// int speedMultipliedScore = Mathf.FloorToInt((float)sc * speedMultiplier * player.rigidbody2D.velocity.magnitude);
		// score += speedMultipliedScore; 
		 */
		score += sc;

		scoreTextMesh.text = "Score: " + score;
	
	}
}
