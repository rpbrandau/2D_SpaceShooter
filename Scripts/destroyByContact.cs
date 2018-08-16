using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	public float hitPoints;

	private PlayerManager playerManager;
	private GameController gameController;



	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) 
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if(gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script.");
		}
		//NEW - Get the instance of the player's PlayerManager script
		GameObject playerManagerObject = GameObject.FindWithTag ("Player");
		if (playerManagerObject != null) 
		{
			playerManager = playerManagerObject.GetComponent<PlayerManager> ();
		}
		if (playerManager == null) 
		{
			Debug.Log ("Cannot find 'PlayerManager' script.");
		}

	}
		

	void OnTriggerEnter2D(Collider2D other)
	{
		//if the other game object is the boundary or enemy, exit
		if (other.CompareTag ("Boundary") || other.CompareTag ("Enemy") || other.CompareTag ("Boss") || other.CompareTag ("Pickup")) 
		{ 
			return;
		}


		if (other.CompareTag ("Ammo")) 
		{
			other.gameObject.SetActive (false); //deactivate ammo when it hits enemies with more than 1hp
			//Destroy(other.gameObject); //removes ammo?
		}
			
		//determine damage dealt
		dealDamage (); 
			

		//if hit and hitpoints are <=0, destroy the object
		if (hitPoints <= 0) 
		{
			if (explosion != null) 
			{
				Instantiate (explosion, transform.position, transform.rotation);
			}
			if (other.CompareTag ("Player")) {
				Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
				gameController.GameOver ();
			}

			gameController.AddScore (scoreValue);
			Destroy (other.gameObject);
			Destroy (gameObject);

		} 
		//if hit and hit points are greater than zero, do nothing, unless it's the player

		else if (hitPoints > 0) 
		{
			if (other.CompareTag ("Player")) 
			{
				Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
				Destroy (other.gameObject);
				gameController.GameOver ();
			}

			return;
		}


	}

	//Change damage dealt based on weapon equipped
	void dealDamage()
	{
		if(playerManager.weapon.name == ("Bullet"))
		{
			hitPoints = hitPoints - 1.0f;
		}
		if (playerManager.weapon.name == ("Laser")) 
		{
			hitPoints = hitPoints - 1.5f;
		}
		if (playerManager.weapon.name == ("Bullet_Spread")) 
		{
			hitPoints = hitPoints - 3.0f;
		}
		if (playerManager.weapon.name == ("Laser_Spread")) 
		{
			hitPoints = hitPoints - 4.5f;
		}
		if (playerManager.weapon.name == ("Missile")) 
		{
			hitPoints = hitPoints - 5.5f;
		}
		if (playerManager.weapon.name == ("Missile_Spread")) 
		{
			hitPoints = hitPoints - 9.0f;
		}
	}

      
}
