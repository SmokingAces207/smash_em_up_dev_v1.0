using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
	/* THE SCORE KEEPER IS STARTED DURING THE BALL CONTROLLERS START DIRECTION METHOD
	   AND IT IS ENDED DURING THE LEVEL MANAGERS CHECK FOR END OF LEVEL AFTER ALL BRICKS
	   ARE DESTROYED */

	public static int score;
	public static bool levelRunning;
	public static float levelTimer;

	private static float bonusTimer;
	private static bool bonusTime;

	private static float bonusDisplayTimer;
	private static bool bonusSpeedEnabled;
	private static bool bonusMultiEnabled;

	public Text timeDisplay;
	public Text scoreDisplay;
	//Center Screen Bonus Display
	public Text bonusDisplay;
	//Top Right Bonus Time Display
	public Text bonusDisplayTime;
	//Center Screen Bonus Display Object
	public GameObject bonusDisplayObject;

	// Use this for initialization
	void Start () {
		timeDisplay.GetComponent<Text> ();
		scoreDisplay.GetComponent<Text> ();
		bonusDisplayObject.GetComponent<GameObject> ();
		bonusDisplay.GetComponent<Text> ();
		bonusDisplayTime.GetComponent<Text> ();
		levelTimer = 100f;
		score = 0;
		bonusTime = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		/*This is set in BallController.cs once the ball is released. 
		And it is stopped in LevelManager.cs when it checks if all bricks are destroyed*/
		if (levelRunning) {
			UpdateLevelTimer ();
		}
		if (bonusTime) {
			BonusTimer ();
		}
		if (bonusSpeedEnabled || bonusMultiEnabled) {
			bonusDisplayObject.SetActive(true);
		}
		if (bonusSpeedEnabled || bonusMultiEnabled) {
			BonusDisplayTimer ();
		}
	}

	void OnGUI () {
		if (levelRunning) {
			timeDisplay.text = "Timer: " + levelTimer.ToString("0.00");
			scoreDisplay.text = "Score: " + score.ToString();
		}
		if (bonusMultiEnabled) {
			bonusDisplay.text = "2X Multiplier";
		}
		if (bonusTime) {
			bonusDisplayTime.text = "2X Time:" + bonusTimer.ToString("0.00");
		} else {
			bonusDisplayTime.text = "";
		}
		if (bonusSpeedEnabled) {
			bonusDisplay.text = "Bonus Speed";
		}
	}

	public static void ResetTimer () {
		levelTimer = 100f;
	}

	private void UpdateLevelTimer () {
		if (levelTimer > 0) {
			levelTimer -= Time.deltaTime;
		} else {
			levelTimer = 0.0f;
		}
	}
	//This handles the powerup multiplier timer in the top right
	private void BonusTimer () {
		bonusTimer -= Time.deltaTime;
		if (bonusTimer < 0.0f) {
			bonusTime = false;
			Debug.Log("Bonus Time ended...");
		}
	}
	//This Handles the 2 second power up title display in the center of the screen
	private void BonusDisplayTimer () {
		bonusDisplayTimer -= Time.deltaTime;
		if (bonusDisplayTimer < 0.0f) {
			bonusDisplayObject.SetActive(false);
			bonusSpeedEnabled = false;
			bonusMultiEnabled = false;
		}
	}
	//This updates the score when each brick is destroyed, this is called in Brick.cs In HandleHits() and PowerUps()
	public static void UpdateScore (string brick) {
		Debug.Log ("brick hit: " + brick);
		if (brick.Contains("1HitBrick")) {
			if (bonusTime) {
				score += 200;
			} else {
				score += 100;
			}
		} else if (brick.Contains("2HitBrick")) {
			if (bonusTime) {
				score += 400;
			} else {
				score += 200;
			}
		} else if (brick.Contains("3HitBrick")) {
			if (bonusTime) {
				score += 600;
			} else {
				score += 300;
			}
		} else if (brick == "2X") {
			bonusMultiEnabled = true;
			bonusDisplayTimer = 2f;
			bonusTimer = 5f;
			bonusTime = true;
		} else if (brick == "BonusSpeed") {
			bonusSpeedEnabled = true;
			bonusDisplayTimer = 2f;
		}
	}
}
