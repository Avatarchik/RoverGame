using UnityEngine;
using System.Collections;

public class PuzzleAnimTrigger : MonoBehaviour {

	public Animator canvasAnim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			canvasAnim.SetTrigger ("FadeForward");
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.tag == "Player") {
			canvasAnim.SetTrigger ("FadeBackward");
		}
	}
}
