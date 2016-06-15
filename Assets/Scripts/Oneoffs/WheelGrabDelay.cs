using UnityEngine;
using System.Collections;

public class WheelGrabDelay : MonoBehaviour {

	BoxCollider myGrabCollider;
	public float grabDelay = 3.0f;

	// Use this for initialization
	void Start () {
		myGrabCollider = gameObject.GetComponent<BoxCollider> ();
		myGrabCollider.enabled = false;
		StartCoroutine (ActivateDelay ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator ActivateDelay(){
		yield return new WaitForSeconds (grabDelay);
		myGrabCollider.enabled = true;
	}
}
