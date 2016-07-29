using UnityEngine;
using System.Collections;
using System;

public class Brick : MonoBehaviour {

	public Sprite[] hitSprites;
	public AudioClip crack;
	public AudioClip explosion;
	public AudioClip powerUp;
	public GameObject smoke;
	public GameObject[] powerUps;
	public static int breakableCount = 0;

	private LevelManager levelManager;
	private int timesHit;
	private bool isBreakable;
	private bool isPowerUp;

	void Start () {
		//If the brick is taged as breakable this is set to true
		isBreakable = (this.tag == "Breakable");
		isPowerUp = (this.tag == "BonusBrick");
		//Keep track of breakable bricks within each scene
		if (isBreakable) {
			breakableCount++;
		}
		timesHit = 0;
		//This allows us to find the object in the scene instead of assigning in the inspector
		//It allows prefabs to work nicely when building new levels.
		levelManager = FindObjectOfType<LevelManager>();
	}

	void Update () {

	}

	void OnCollisionEnter2D (Collision2D hit) {
		if (isBreakable) {
			HandleHits();
		} else if (isPowerUp) {
			PowerUps();
		}
	}

	void HandleHits () {
		timesHit++;
		int maxHits = hitSprites.Length + 1;

		//Change audio in inspector for bonus brick put it outside of (isBreakable) condition
		if (this.gameObject.name != "1hit" && !(timesHit >= maxHits)) {
			//Spawns the audio source where the brick was, so it does not get destroyed when the brick does
			AudioSource.PlayClipAtPoint (crack, transform.position, 4.0f);
		} else if (timesHit >= maxHits) {
			AudioSource.PlayClipAtPoint (explosion, transform.position, 0.65f);
		}

		print ("Bricks left: " + breakableCount);

		//this.gameobject is checking if it exists, to avoid an error. This may cause an issue Later
		//WARNING!!!
		if (timesHit >= maxHits && this.gameObject) {
			//If the game object is not null and the brick has taken its maximum hits or greater
			//Reduce the breakable count and destroy that object
			breakableCount--;
			//Sends a message to level manger to see if all bricks have been destroyed
			levelManager.BrickDestroyed();
			InstantiateSmoke ();
			ScoreKeeper.UpdateScore (this.gameObject.name);
			Destroy(this.gameObject);
			//Instantiate or set active a random power up
		} else {
			//If the block is not ready to be destroyed yet, load next sprite
			LoadSprites();
		}
	}

	void PowerUps () {
		timesHit++;
		int maxHits = hitSprites.Length + 1;

		if (!(timesHit >= maxHits)) {
			AudioSource.PlayClipAtPoint (crack, transform.position, 4.0f);
		} else if (timesHit >= maxHits) {
			AudioSource.PlayClipAtPoint (powerUp, transform.position, 1.0f);
		}

		if (timesHit >= maxHits && this.gameObject) {
			InstantiateSmoke ();
			InstantiatePowerUp ();
			Destroy(this.gameObject);
		} else {
			LoadSprites();
		}
	}

	void InstantiateSmoke () {
		GameObject smokePuff = (GameObject)Instantiate(smoke, this.transform.position , Quaternion.identity);
		smokePuff.GetComponent<ParticleSystem> ().startColor = this.GetComponent<SpriteRenderer> ().color;
	}

	void InstantiatePowerUp () {
		GameObject powerUp = (GameObject)Instantiate(powerUps[0], this.transform.position, Quaternion.identity);
	}

	void LoadSprites () {
		//Loads Sprite index of 0 if hit once, which is the 1hit sprite image.
		int spriteIndex = timesHit - 1;

		if (hitSprites[spriteIndex]) {
			//Get the spriteRenderer component attached to this brick object and set change the sprite
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		} else {
			Debug.LogError("Error 01: Block Sprite Missing Array Index, Please Check Prefab");
		}
	}
}