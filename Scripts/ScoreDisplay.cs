using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {
	/*This class keeps track of what score was given on the previous level and what level is being loaded next
	The singleton method should be implemented to keep only gameobject storing this information.
	*/

	public static int currentLevel;

	public Text score;

	void Start () {
		score.GetComponent<Text> ();
		DisplayScore ();
	}

	public void DisplayScore () {
		score.text = "Congratulations\nLEVEL   " + currentLevel.ToString() + "   PASSED\nSCORE   :   " + ScoreKeeper.score.ToString();
	}
}
