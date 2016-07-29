using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public AudioClip powerCollected;
	public GameObject powerBall;
	public GameObject Ball;

	private Rigidbody2D ballRB2D;

	void Start () {
		ballRB2D = Ball.GetComponent<Rigidbody2D>();
	}

	void OnTriggerEnter2D (Collider2D ballHit) {
		if (ballHit.gameObject.tag == "Ball") {
			AudioSource.PlayClipAtPoint (powerCollected, transform.position, 1.0f);
			int randomPower = Random.Range(0, 2);

			Vector2 bonusPower = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));

			if (randomPower == 0) {
				ScoreKeeper.UpdateScore("2X");
				//GameObject powerBallSpawn = (GameObject)Instantiate(powerBall, this.transform.position, Quaternion.identity);
			} else { 
				ScoreKeeper.UpdateScore("BonusSpeed");
				ballRB2D.velocity += bonusPower;
			}
			Destroy(this.gameObject);

		}
	}
}
