using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol {
	public class PuzzleAnimHandler : MonoBehaviour {

		public PuzzleManager puzzleManager;
		public InteractiblePuzzle myInteracter;
		public Light puzzleLight;
		public Color lightBlinkColor;
		private bool blink;
		private bool activate;
		private float lightActivateTime = 1.5f;
		private float lightBlinkTime = 0.75f;
		private float lightBlinkIntensity = 8.0f;
		private float lightStartIntensity;
		private float currentTime;
		private int blinkNumber;

		// Use this for initialization
		void Start () {
			if (puzzleLight != null) {
				lightStartIntensity = puzzleLight.intensity;
				puzzleLight.intensity = 0.0f;
			}
		}
		
		// Update is called once per frame
		void Update () {
			if (puzzleLight != null){
				if (activate) {
					if (currentTime < lightActivateTime) {
						currentTime += Time.unscaledDeltaTime;
						float lerp = currentTime / lightActivateTime;
						puzzleLight.intensity = Mathf.Lerp (0.0f, lightStartIntensity, lerp);
					} else {
						currentTime = 0.0f;
						activate = false;
					}
				} else if (blink) {
					if (blinkNumber == 0) {
						if (currentTime < lightBlinkTime) {
							currentTime += Time.unscaledDeltaTime;
							float lerp = currentTime / lightBlinkTime;
							puzzleLight.intensity = Mathf.Lerp (0.0f, lightBlinkIntensity, lerp);
						} else {
							currentTime = 0.0f;
							blinkNumber = 1;
						}
					} else if (blinkNumber == 1) {
						if (currentTime < lightBlinkTime) {
							currentTime += Time.unscaledDeltaTime;
							float lerp = currentTime / lightBlinkTime;
							puzzleLight.intensity = Mathf.Lerp (lightBlinkIntensity, 0.0f, lerp);
						} else {
							currentTime = 0.0f;
							blinkNumber = 0;
						}
					}
				}
			}
		}

		public void ActivateLight() {
			activate = true;
		}

		public void BlinkLight() {
			blink = true;
			puzzleLight.color = lightBlinkColor;
		}

		public void ScaledTrue() {
			myInteracter.puzzleScaled = true;

		}
		public void ScaledFalse() {
			myInteracter.puzzleScaled = false;
		}
	}
}
