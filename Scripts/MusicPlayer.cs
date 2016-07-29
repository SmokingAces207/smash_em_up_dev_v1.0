using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	private static MusicPlayer instance = null;
	
	void Awake () {
		Debug.Log("Play on Awake " + GetInstanceID());
		//If there is an instance of Music Player in the scene, which is only true in the start scene
		//The music player will be destroyed as not to create duplicates.
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player destroyed!");
		} else {
			//Instance is set to null so this runs first, and sets instance to this Music Player Object
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}
	
	void Start () {
		Debug.Log("Play on Start " + GetInstanceID());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}