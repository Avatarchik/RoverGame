using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol {
	public class PuzzleTriggerZone : MonoBehaviour {

		private Transform puzzleCanvas;
		public bool inPuzzleZone;
		public float minPuzzleDistance;
		public float maxPuzzleDistance;
		public float playerDistance; 

		// Use this for initialization
		void Start () {
			puzzleCanvas = transform.GetComponentInParent<InteractiblePuzzle> ().myPuzzleCanvas.transform;
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void OnTriggerStay (Collider other) {
			if (other.transform.parent.tag == "Player") {
				playerDistance = Mathf.Abs (Vector3.Distance (puzzleCanvas.position, other.transform.position));
				if (playerDistance >= minPuzzleDistance && playerDistance < maxPuzzleDistance) {
					inPuzzleZone = true;
				} else {
					inPuzzleZone = false;
				}
			}
		}

		void OnTriggerExit (Collider other) {
			inPuzzleZone = false;
		}
	}
}
