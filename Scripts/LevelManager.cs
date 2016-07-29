using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void LoadLevel (string name) {
		Debug.Log ("Level Loaded: " + name);
		Brick.breakableCount = 0;
        SceneManager.LoadScene(name);
	}
	
	public void Quit () {
		Application.Quit();
	}

	public void LoadNextLevel() {
		Brick.breakableCount = 0;
		ScoreKeeper.ResetTimer ();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
	}

	public void BrickDestroyed () {
		if (Brick.breakableCount <= 0) {
			ScoreKeeper.levelRunning = false;
			ScoreDisplay.currentLevel = SceneManager.GetActiveScene().buildIndex;
			LoadNextLevel();
		}
	}
}