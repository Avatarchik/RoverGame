using UnityEngine;
using System.Collections;

namespace Sol {
	public class PuzzleAnimTrigger : MonoBehaviour {

		Animator canvasAnim;
		public bool enter;
		[HideInInspector]
		public bool activateLight;
		public bool blink;
		public Light puzzleLight;
		public Color lightBlinkColor;
		private float currentTime;
		private float currentTime02;
		private int blinkNumber;
		private float lightActivateTime = 1.5f;
		private float lightBlinkTime = 0.75f;
		private float lightBlinkIntensity = 3.0f;
		private float lightStartIntensity;
		private InteractiblePuzzle myInteracter;

		// Use this for initialization
		void Start () {
			canvasAnim = gameObject.GetComponent<Animator> ();
			myInteracter = transform.GetComponentInParent<InteractiblePuzzle> ();
			if (puzzleLight != null) {
				lightStartIntensity = puzzleLight.intensity;
				puzzleLight.intensity = 0.0f;
			}
		}
		
		// Update is called once per frame
		void Update () {
			if (puzzleLight != null){
				if (activateLight && currentTime < lightActivateTime) {
					currentTime += Time.unscaledDeltaTime;
					float lerp = currentTime / lightActivateTime;
					puzzleLight.intensity = Mathf.Lerp (0.0f, lightStartIntensity, lerp);
				} else if (blink) {
					puzzleLight.color = lightBlinkColor;
					if (blinkNumber == 0) {
						if (currentTime02 < lightBlinkTime) {
							currentTime02 += Time.unscaledDeltaTime;
							float lerp = currentTime02 / lightBlinkTime;
							puzzleLight.intensity = Mathf.Lerp (0.0f, lightBlinkIntensity, lerp);
						} else {
							currentTime02 = 0.0f;
							blinkNumber = 1;
						}
					} else if (blinkNumber == 1) {
						if (currentTime02 < lightBlinkTime) {
							currentTime02 += Time.unscaledDeltaTime;
							float lerp = currentTime02 / lightBlinkTime;
							puzzleLight.intensity = Mathf.Lerp (lightBlinkIntensity, 0.0f, lerp);
						} else {
							currentTime02 = 0.0f;
							blinkNumber = 0;
						}
					}
				}
			}
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

		public void ScaledTrue () {
			myInteracter.puzzleScaled = true;
		}
		public void ScaledFalse () {
			myInteracter.puzzleScaled = false;
		}
	}
}
