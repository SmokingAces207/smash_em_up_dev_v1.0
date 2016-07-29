using UnityEngine;
using System.Collections;

public class LoseCollider : MonoBehaviour {

    private LevelManager levelManager;

    void OnTriggerEnter2D (Collider2D ball) {
		levelManager = FindObjectOfType<LevelManager>();
		if (ball.gameObject.name == "Ball") {
			print("Trigger Enter, Game Over!");
        	levelManager.LoadLevel("Lose Screen");
		}
        
    }
}