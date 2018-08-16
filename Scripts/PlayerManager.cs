using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	//text variables
	private int score;

	//ship variables
	public float ship_speed;
	private Rigidbody2D rb2d;
	Animator anim;

	//ammo variables
	public GameObject [] weapons;
	public GameObject weapon;
	public Transform shotSpawn;
	public int powerupCount;

	Animator bulletAnim;
	public float fireRate;
	private float nextFire;
	private AudioSource audioSource;

	private GameController gameController;



	// Use this for initialization
	void Start () 
	{
		powerupCount = 0;
		anim = GetComponent<Animator> ();	
		rb2d = GetComponent<Rigidbody2D> ();
		audioSource = GetComponent<AudioSource> ();
		weapon = weapons [powerupCount];
		weapons [powerupCount].gameObject.SetActive (true);

		//NEW
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) 
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if(gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script.");
		}

	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		//Movement
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		//Floaty Controls
		//rb2d.AddForce (movement * speed);

		//Tighter Controls
		rb2d.velocity = movement * ship_speed;
	}

	void Update ()
	{
		//Fire Shots
		if (Input.GetButton ("Fire1") && Time.time > nextFire) 
		{
			Fire ();
		}

		//CheckWeapons ();

		//Animations
		//Test 1 for animations
		/* 
		if (Input.GetAxis (KeyCode.LeftArrow))
		{
				anim.SetBool("left", true);
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow)) 
		{
			anim.SetBool ("left", false);
		}
		
		if (Input.GetKeyDown (KeyCode.RightArrow))
		{
				anim.SetBool ("right", true);
		}
		if (Input.GetKeyUp (KeyCode.RightArrow)) 
		{
				anim.SetBool ("right", false);
		}
		*/

		//Test 2 for animations
		if (Input.GetAxis ("Horizontal") < 0) 
		{
			anim.SetBool ("left", true);
			anim.SetBool ("right", false);
		}

		if (Input.GetAxis ("Horizontal") > 0) 
		{
			anim.SetBool ("right", true);
			anim.SetBool ("left", false);
		}

		if (Input.GetAxis ("Horizontal") == 0) 
		{
			anim.SetBool ("left", false);
			anim.SetBool ("right", false);
		}



	}


	void OnTriggerEnter2D(Collider2D other) 
	{
		//remove pickup if ship hits it
		if (other.gameObject.CompareTag ("Pickup")) 
		{
			other.gameObject.SetActive (false);
			//NEW
			gameController.AddScore(100);
			powerupCount++;
			powerUp (powerupCount);
		}

		//remove player if contact is made with enemy
		if (gameObject.CompareTag ("Enemy")) 
		{
			gameObject.SetActive (false);
		}
	}

	void Fire()
	{
		nextFire = Time.time + fireRate;
		Instantiate (weapon, shotSpawn.position, shotSpawn.rotation);
		audioSource.Play ();
	}

	//PowerUps grant weapon improvements
	void powerUp(int x)
	{

		if (x == 1) 
		{
			weapons [0].gameObject.SetActive (false); //disable bullet
			weapons [1].gameObject.SetActive (true); //enable laser
			fireRate = 0.2f;
		}
		if (x == 2) 
		{
			weapons [1].gameObject.SetActive (false); // disable laser
			weapons [2].gameObject.SetActive (true); //enable 3x bullet
			fireRate = 0.25f;
		}
		if (x == 3) 
		{
			weapons [2].gameObject.SetActive (false); // disable 3x bullet
			weapons [3].gameObject.SetActive (true); //enable missile
			fireRate = 0.3f;
		}
		if (x == 4) 
		{
			weapons [3].gameObject.SetActive (false); // disable missile
			weapons [4].gameObject.SetActive (true); //enable 3x laser
			fireRate = 0.2f;
		}
		if (x == 5) 
		{
			weapons [4].gameObject.SetActive (false); // disable 3x laser
			weapons [5].gameObject.SetActive (true); //enable 3x missile
			fireRate = 0.3f;
		}

		if (x == 6) //no more weapons, keep missle spread, increase fire rate.
		{
			powerupCount = 5;
			fireRate -= 0.01f;
		}
		weapon = weapons [powerupCount];
	}
		
}
