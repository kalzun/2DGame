using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour 
{
	public float maxSpeed;
	private float jumpForce;
	

	Animator anim;

	bool facingRight = true;

	// Ground-check
	bool grounded = false;
	bool jumping = false;

	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;

	bool notCrouching = true;
	// Box Collider to reduce when crouching, not working as is. TODO
	/* BoxCollider2D playerCollision;
	playerCollision = ((BoxCollider2D)collider).size.x;
	public float crouchHeight = 0.11f;
	float standHeight = 0.43f;
	float crouchOffset = -0.056f;
	*/
	
	private GameManager gm;
	
	void Start () 
	{
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();

		anim = GetComponent<Animator> ();

	}

	void Update ()
	{
		// Crouching
		if (Input.GetKeyDown (KeyCode.DownArrow)) 
		{
			if (notCrouching == true) 
			{
				Crouching ();
			}
			else if (notCrouching == false)
			{
				StopCrouching ();
			}
		}
		// JUMP
		if (grounded && Input.GetKeyDown (KeyCode.LeftShift) || Input.GetMouseButtonDown(0)) 
		{
			anim.SetBool ("Ground", false);
			jumpForce = 400;
			jumping = true;
			grounded = false;
			
		}

	}


	// Update is called once per frame
	void FixedUpdate () 
	{			
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

		float move = Input.GetAxis ("Horizontal");	

		anim.SetFloat ("Speed", Mathf.Abs (move));

		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
				

		// Ground-check
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		if (jumping) 
		{
			rigidbody2D.AddForce (new Vector2 (0, jumpForce));
			jumping = false;
		}
				

		// Facing right or left
		if (move > 0 && !facingRight) 
		{
			Flip ();
		} else if (move < 0 && facingRight) 
		{
			Flip ();
		}
	}
				
		


	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Crouching()
	{
		Debug.Log ("Crouching");
		notCrouching = false;
		anim.CrossFade ("Crouch", 0.5f);
		/* 
		 * 
		playerCollision.size = new Vector2 (playerCollision.size.x, crouchHeight);
		playerCollision.center = new Vector2 (0, crouchOffset);

		*/

	}

	void StopCrouching()
	{
		notCrouching = true;
		anim.CrossFade ("Idle", 0.5f);

	}



	void OnCollisionEnter2D(Collision2D obstacle) 
	{
		if (obstacle.gameObject.CompareTag ("Obstacle")) 
		{
			Debug.Log ("Collided with something, lets die!");
			// We die!

			PlayerScoreFlappy score = GetComponent<PlayerScoreFlappy>();
			if (score)
			{
				score.UpdateHighScoreOnDeath ();
			}
			gm.PlayerDied (score);
		}
	}
}
	