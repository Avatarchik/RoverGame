using UnityEngine;
using System.Collections;

public class PuzzleAnimTrigger : MonoBehaviour {

	Animator canvasAnim;
	public bool enter;
	[HideInInspector]
	public bool fadeLight;
	public Light puzzleLight;
	private float currentTime;
	private float lightFadeTime = 1.5f;
	private float lightStartIntensity;

	// Use this for initialization
	void Start () {
		canvasAnim = gameObject.GetComponent<Animator> ();
		if (puzzleLight != null) {
			lightStartIntensity = puzzleLight.intensity;
			puzzleLight.intensity = 0.0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (puzzleLight != null && fadeLight && currentTime < lightFadeTime) {
			currentTime += Time.unscaledDeltaTime;
			float lerp = currentTime / lightFadeTime;
			puzzleLight.intensity = Mathf.Lerp (0.0f, lightStartIntensity, lerp);
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
}
