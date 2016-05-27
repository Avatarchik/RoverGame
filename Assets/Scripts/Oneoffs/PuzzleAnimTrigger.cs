using UnityEngine;
using System.Collections;

public class PuzzleAnimTrigger : MonoBehaviour {

	public Animator canvasAnim;
	public bool enter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			enter = true;
			canvasAnim.SetTrigger ("FadeForward");
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.tag == "Player") {
			enter = false;
			canvasAnim.SetTrigger ("FadeBackward");
		}
	}
}
