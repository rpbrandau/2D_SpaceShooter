using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public float maxHitPoints;
	public float currentHitPoints;
	public bool isBoss;
	public bool destroyed;
	Collider2D other;

	/*Copied over from DestroyedByContact*/
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	//destroyByContact enemy;

	// Use this for initialization
	void Start () {
		destroyed = false;
		currentHitPoints = maxHitPoints;
		//enemy = GetComponent<destroyByContact> ();

		/*copied over from destroyedbycontact*/
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
	void Update () {

	}

	public void TakeDamage(float amount)
	{
		currentHitPoints -= amount;

		if (currentHitPoints <= 0 && !destroyed) 
		{
			Death ();
		}
	}

	void Death()
	{
		destroyed = true;
		//enemy.OnTriggerEnter2D(other);
		OnTriggerEnter2D (other);
	}

	/*
		Copied over from DestroyByContact
	*/
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag("Boundary") || other.CompareTag("Enemy")) 
		{
			return;
		}

		if (explosion != null) 
		{
			Instantiate (explosion, transform.position, transform.rotation);
		}

		if(other.CompareTag("Player"))
		{
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		}

		gameController.AddScore (scoreValue);
		Destroy (other.gameObject);
		Destroy (gameObject);
	}

}
