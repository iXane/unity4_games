using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	// Loads the specified level //
	public void LoadLevel(string name)
	{
		//Debug.Log("Level load requested for " + name);
		Brick.breakableCount = 0;
		Application.LoadLevel(name);
	}
	
	// Loads the next level //
	public void LoadNextLevel()
	{
		Brick.breakableCount = 0;
		Application.LoadLevel(Application.loadedLevel + 1);
	}
	
	// Closes Game //
	public void QuitRequest()
	{
		//Debug.Log("Quit requested");
		Application.Quit();
	}
	
	public void BrickDestroyed()
	{
		if (Brick.breakableCount <= 0)
			LoadNextLevel();
	}
}
