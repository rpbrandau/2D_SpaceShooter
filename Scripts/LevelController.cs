using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

	public int index;
	public int indexToLoad;
	public string sceneName;
	private Scene currentScene;


	void Start () {
		currentScene = SceneManager.GetActiveScene (); 
		index = currentScene.buildIndex;
		indexToLoad = index + 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (index == 0)
			nextScene (indexToLoad);
			
		
	}

	void nextScene(int indexToLoad){
		SceneManager.LoadScene (indexToLoad);
	}
}
