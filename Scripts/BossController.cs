using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {


	private bool isBoss;
	public bool destroyed;
	public float currentHP;
	private destroyByContact HPController;
	private GameController gameController;

	// Use this for initialization
	void Start () {
		//isBoss = true;
		destroyed = false;

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) 
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if(gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script.");
		}

		GameObject HPControllerObject = GameObject.FindWithTag ("Boss");
		if (HPControllerObject != null) 
		{
			HPController = HPControllerObject.GetComponent <destroyByContact>();
		}
		if(HPController == null)
		{
			Debug.Log ("Cannot find 'destroyByContact' script.");
		}

		currentHP = HPController.hitPoints;

	}
	
	// Update is called once per frame
	void Update () {
		
		currentHP = HPController.hitPoints; //update hitpoints every frame

		if (currentHP <= 0) 
		{
			destroyed = true;
			gameController.bossAlive = false;
		}
		/*
		if (destroyed == true) //tell game controller that boss has died
		{
			gameController.bossAlive = false;
		}
		*/
		
	}


}
