using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public float levelSpeed = 3.0f;

	public void PlayerDied(PlayerScoreFlappy score)
	{
		// This simply reloads level 0.
		// TODO Make this a bit more proper.
		Application.LoadLevel (0);

	}
}
