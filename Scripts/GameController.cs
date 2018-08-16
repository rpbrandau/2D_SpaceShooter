using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	//enemy spawns
	//public GameObject hazard;
	public GameObject[] hazards;
	public GameObject boss;
	public GameObject powerUp;
	public float powerUpTimer; //used to determine when to spawn a powerup
	public Vector2 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public int waveCount; // keep track of waves

	public bool spawnBoss; // boss flag - to spawn boss
	public bool bossAlive; // boss flag - to transition level
	public bool bossSpawned;
	private int sceneIndex;
	private BossController bossController;

	//text
	public Text scoreText;
	public Text winText;
	public Text restartText;
	public Text gameOverText;


	private bool gameOver;
	private bool restart;
	private int scoreTotal;
	private int score;

	void Start ()
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		score += scoreTotal;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
		bossAlive = true;
		spawnBoss = false;
		bossSpawned = false;
		sceneIndex = SceneManager.GetActiveScene ().buildIndex;
		waveCount = 0;
		powerUpTimer = Random.Range (10, 20);
	}

	void Update ()
	{
		spawnPowerUp();

		if (restart)  //if restart is true
		{
			if (Input.GetKeyDown (KeyCode.R)) //check to see if R is pressed
			{ 
				int scene = SceneManager.GetActiveScene ().buildIndex; //get the current scene value
				SceneManager.LoadScene (scene, LoadSceneMode.Single);  //load current level
			}
		}

		if (spawnBoss) //if spawnBoss is true, spawn the boss
		{
			SpawnBoss ();
			//New
			GameObject bossControllerObject = GameObject.FindWithTag("Boss");
			if (bossControllerObject != null) {
				bossController = bossControllerObject.GetComponent<BossController> ();
			}
			if (bossController == null) {
				Debug.Log ("Cannot find 'BossController' script.");
			}

		}

		//if the boss has been spawned, check to see if it is alive
		if (bossSpawned) 
		{
			CheckBoss ();
		}

		if (bossAlive == false) //if boss has been killed, load next scene
		{
			scoreTotal += score;
			Scene scene = SceneManager.GetActiveScene ();
			StartCoroutine (ChangeLevel (scene));
			//bossAlive = true;
		}



	}

	void CheckBoss()
	{
		bossAlive = bossController.destroyed;
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while(!spawnBoss) //while spawnBoss is false, spawn hazards
		{
			for (int i = 0; i < hazardCount; i++) 
			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector2 spawnPosition = new Vector2 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y);
				Instantiate (hazard, spawnPosition, transform.rotation);
				yield return new WaitForSeconds (spawnWait);

				if (sceneIndex == 0) 
				{
					if (score >= 250 ) 
					{
						//new - trying to get boss to spawn after all other enemies have cleared out
						yield return new WaitForSeconds (5);
						spawnBoss = true;
					}
				}

				if (sceneIndex == 1) 
				{
					if (score >= 800 ) 
					{
						spawnBoss = true;
						//bossAlive = true;
					}
				}

				if (sceneIndex == 2) 
				{
					if (score >= 2500 ) 
					{
						spawnBoss = true;
						//bossAlive = true;
					}
				}

				if (sceneIndex == 3) 
				{
					if (score >= 8000 ) 
					{
						spawnBoss = true;
						//bossAlive = true;
					}
				}

			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver)
			{
				restartText.text = "Press 'R' to Restart";
				restart = true;
				break;
			}
		}
	}


	void spawnPowerUp()
	{
		if (powerUpTimer <= Time.time) 
		{
			Vector2 spawnPosition = new Vector2 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y);
			Instantiate (powerUp, spawnPosition, transform.rotation);
			powerUpTimer = Random.Range (Time.time + 10, Time.time + 20);
		}
	}

	public void SpawnBoss() //spawn the boss in a similar way to waves of hazards
	{
		if (!bossSpawned) 
		{
			Vector2 spawnPosition = new Vector2 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y);
			Instantiate (boss, spawnPosition, transform.rotation);
		}
		bossSpawned = true;
	}

	public void GameOver()
	{
		gameOverText.text = "Game Over, Man!";
		gameOver = true;
	}
		

	IEnumerator ChangeLevel(Scene scene)
	{
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene (scene.buildIndex + 1);
	}

}
