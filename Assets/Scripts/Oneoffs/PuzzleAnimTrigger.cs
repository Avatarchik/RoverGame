using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzleAnimTrigger : MonoBehaviour {

	public List<Animator> canvasAnimators;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			foreach (Animator anim in canvasAnimators) {
				anim.SetTrigger ("FadeForward");
			}
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.tag == "Player") {
			foreach (Animator anim in canvasAnimators) {
				anim.SetTrigger ("FadeBackward");
			}
		}
	}
}
