using UnityEngine;
using System.Collections;

public class Smoke : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject, 4);
	}
}
