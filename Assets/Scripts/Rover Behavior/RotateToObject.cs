using UnityEngine;
using System.Collections;

public class RotateToObject : MonoBehaviour {

	public Transform targetObject;
	private Quaternion startRotation;
	private Quaternion targetRotation;
	private Vector3 targetDirection;
	private float currentTime;
	public float rotateTime;
	public bool instantRotate;
	private bool rotate;


	// Use this for initialization
	void Start () {
		if (instantRotate) {
			RotateTo (targetObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (rotate) {
			if (instantRotate) {
				targetDirection = transform.position - targetObject.position;
				targetRotation = Quaternion.LookRotation (targetDirection);
				transform.rotation =  Quaternion.LookRotation (targetDirection, targetRotation * Vector3.up);
			} else if (currentTime < rotateTime) {
				currentTime += Time.unscaledDeltaTime;
				float lerp = currentTime / rotateTime;
				transform.rotation = Quaternion.Slerp (startRotation, targetRotation, lerp);
			} else {
				rotate = false;
				currentTime = 0.0f;
				if (gameObject.GetComponent<MouseLook> () != null) {
					gameObject.GetComponent<MouseLook> ().enabled = true;
				}
			}
		}
	}

	public void RotateTo (Transform target) {
		targetObject = target;
		startRotation = transform.rotation;
		targetDirection = targetObject.position - transform.position;
		targetRotation = Quaternion.LookRotation (targetDirection);
		targetRotation = Quaternion.LookRotation (targetDirection, targetRotation * Vector3.up);
		rotate = true;
		if (gameObject.GetComponent<MouseLook> () != null) {
			gameObject.GetComponent<MouseLook> ().enabled = false;
		}
	}
}
