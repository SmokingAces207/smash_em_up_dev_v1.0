using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {

	private bool autoPlay;

	private BallController ball;

	//private Rigidbody2D rb;
	public float speed;

    // Use this for initialization
    void Start () {
		//rb = GetComponent<Rigidbody2D>();
		//This allows us to find the object in the scene instead of assigning in the inspector
		//It allows prefabs to work nicely when building new levels.
		ball = FindObjectOfType<BallController>();
		//AUTO PLAY!!!!!!!!!
		autoPlay = true;
	}

	// Update is called once per frame
	void Update () {
		if (!autoPlay) {
			KeyBoardControls ();
		} else {
			AutoPlay ();
		}
	}

	void AutoPlay () {
		Vector3 paddlePos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		Vector3 ballPos = ball.transform.position;
		paddlePos.x = Mathf.Clamp(ballPos.x, 1f, 15f);
		this.transform.position = paddlePos;
	}

	void MouseControls () {
		//Get paddle starting position
        Vector3 paddlePos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
        //Get the paddles current position relevant to the games block spacing
		float mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
        //Limit the value so that it does not go off screen
        paddlePos.x = Mathf.Clamp(mousePosInBlocks, 1f, 15f);
        //Set the current paddle position to move the object
        this.transform.position = paddlePos;
	}

	void KeyBoardControls () {
		Vector3 paddlePos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		float xPos = gameObject.transform.position.x + (Input.GetAxis ("Horizontal") * speed * Time.smoothDeltaTime);
		paddlePos.x = Mathf.Clamp(xPos, 1f, 15f);
		this.transform.position = paddlePos;
	}
}