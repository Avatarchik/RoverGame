using UnityEngine;
using System.Collections;

public class RotateTowardsPlayer : MonoBehaviour {

	Transform playerView;
	public float turnSpeed;

	// Use this for initialization
	void Start () {
		playerView = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		rotateTowards (playerView.position);
	}

	protected void rotateTowards(Vector3 to) {

		Quaternion _lookRotation = 
			Quaternion.LookRotation((to - transform.position).normalized);

		//over time
		transform.rotation = 
			Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turnSpeed);

		//instant
		transform.rotation = _lookRotation;
	}
}
