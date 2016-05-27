using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol {
	public class PuzzleAnimHandler : MonoBehaviour {

		Animator canvasAnim;
		public PuzzleManager puzzleManager;
		public Collider myPuzzleTrigger;
		public bool enter;
		public bool blink;
		private bool activateLight;
		public int scaleCount = 0;
		public Light puzzleLight;
		public Color lightBlinkColor;
		private float currentTime;
		private float currentTime02;
		private int blinkNumber;
		private float lightActivateTime = 1.5f;
		private float lightBlinkTime = 0.75f;
		private float lightBlinkIntensity = 3.0f;
		private float lightStartIntensity;
		private GameObject puzzleCanvas;
		private InteractiblePuzzle myInteracter;
		private List <GameObject> silhouettes = new List<GameObject> ();

		// Use this for initialization
		void Start () {
			canvasAnim = gameObject.GetComponent<Animator> ();
			myInteracter = transform.GetComponentInParent<InteractiblePuzzle> ();
			puzzleCanvas = myInteracter.myPuzzleCanvas;
			if (puzzleLight != null) {
				lightStartIntensity = puzzleLight.intensity;
				puzzleLight.intensity = 0.0f;
			}
			silhouettes.Add(transform.GetComponentInParent<InteractiblePuzzle>().silhouetteInteractible);
			silhouettes.Add(transform.GetComponentInParent<InteractiblePuzzle>().silhouetteSeen);
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

		public void FirstInteract() {
			canvasAnim.SetTrigger ("FadeForward");
			activateLight = true;
			myPuzzleTrigger.enabled = true;
		}

		public void ScaledTrue(){
			myInteracter.puzzleScaled = true;
			if (scaleCount == 2) {
				print (scaleCount);
				gameObject.GetComponentInParent<InteractiblePuzzle> ().Interact ();
				StartCoroutine (DelayInitialize ());
			}

		}
		public void ScaledFalse(){
			myInteracter.puzzleScaled = false;
			scaleCount++;
		}

		public IEnumerator DelayInitialize() {
			yield return new WaitForSeconds (0.1f);
			puzzleManager.InitializePuzzle (puzzleCanvas);
		}
	}
}
