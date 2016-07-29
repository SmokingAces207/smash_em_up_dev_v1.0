using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	public Transform powerBallPrefab;

    private PaddleController paddle;
    private Rigidbody2D rb;
    private AudioSource brickBreak;
    private AudioSource broundHit;
    private bool hasStarted = false;

    private Vector3 paddleToBallVector;
	// Use this for initialization
	void Start () {
		//This allows us to find the object in the scene instead of assigning in the inspector
		//It allows prefabs to work nicely when building new levels.
		paddle = FindObjectOfType<PaddleController>();
        paddleToBallVector = this.transform.position - paddle.transform.position;
		rb = GetComponent<Rigidbody2D>();
		//Here I am getting the COMPONENT(S) of audiosource attached to the ball object and storing them in an array
		AudioSource[] audioSources = GetComponents<AudioSource>();
		brickBreak = audioSources[0];
		broundHit = audioSources[1];
		//audioClip2 = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasStarted) {
			//Locking the ball relative to the paddle on start
			this.transform.position = paddle.transform.position + paddleToBallVector;
			//If mouse is pressed then the ball is launched once
			if (Input.GetMouseButtonDown(0)) {
        		print("Mouse Clicked, Launched Ball");
        		StartDirection();
        		hasStarted = true;
			} else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
				print("W Pressed, Launched Ball");
				StartDirection();
        		hasStarted = true;
        	}
		}
	}

	void StartDirection () {
		//Starts Score Keeper
		ScoreKeeper.levelRunning = true;
		int startDirection = Random.Range(0, 2);
		Debug.Log ("Start Direction: " + startDirection);
		if (startDirection == 1) {
			rb.velocity = new Vector2(3f, 8f);
		} else {
			rb.velocity = new Vector2(-3f, 8f);
		}
	}

	void OnCollisionEnter2D (Collision2D brickHit) {
		//Add a random tweak to the balls direction to avoid boring gameplay and poor loops
		Vector2 tweak = new Vector2(Random.Range(0f, 0.3f), Random.Range(0f, 0.3f));
		Vector2 powerBallTweak = new Vector2(Random.Range(0.5f, 1f), Random.Range(0.3f, 1f));
		//Vector2 forceTweak = new Vector2 (Random.Range (0f, 0.5f), Random.Range (0f, 0.5f));

		//Avoid playing audio before the ball has started moving
		if (hasStarted) {
			if (brickHit.gameObject.tag == "Breakable") {
				brickBreak.Play();
			} else {
				broundHit.Play();
			}
			//Here we add the random velocity range on collision
			rb.velocity += tweak;
			if (brickHit.gameObject.tag == "PowerBall") {
				Debug.Log("Power Ball Hit:" + powerBallTweak);
				//Transform powerBall = powerBallPrefab as Transform;
				//Physics.IgnoreCollision(powerBall.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
			}
			//rb.AddForce(forceTweak, ForceMode2D.Impulse);
		}
	}
}