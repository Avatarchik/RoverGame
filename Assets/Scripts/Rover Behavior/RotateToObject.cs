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
			 if (currentTime < rotateTime) {
				currentTime += Time.unscaledDeltaTime;
				float lerp = currentTime / rotateTime;
				transform.rotation = Quaternion.Slerp (startRotation, targetRotation, lerp);
			} else {
				rotate = false;
				currentTime = 0.0f;
				instantRotate = true;
			}
		}

		if (instantRotate) {
			targetDirection = targetObject.position - transform.position;
			if (gameObject.GetComponent<Canvas> () != null) {
				targetDirection *= -1;
			}
			targetRotation = Quaternion.LookRotation (targetDirection);
			transform.rotation =  Quaternion.LookRotation (targetDirection, targetRotation * Vector3.up);
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

	public void ConvertAngles(Vector3 currentEulers) {
		float desiredRotationX = 0.0f;
		float desiredRotationY = 0.0f;
		if (currentEulers.y < 360 && currentEulers.y >= 270) {
			desiredRotationX = 360 - currentEulers.y;
		} else {
			desiredRotationX = currentEulers.y;
		}
		if (currentEulers.x < 360 && currentEulers.x >= 270) {
			desiredRotationY = 360 - currentEulers.x;
		} else {
			desiredRotationY = -currentEulers.x;
		}
		gameObject.GetComponent<MouseLook> ().rotationX = desiredRotationX;
		gameObject.GetComponent<MouseLook> ().rotationY = desiredRotationY;
	}

	public void EndRotation(){
		instantRotate = false;
		if (gameObject.GetComponent<MouseLook> () != null) {
			ConvertAngles (transform.localEulerAngles);
			gameObject.GetComponent<MouseLook> ().enabled = true;
		}
	}
}
