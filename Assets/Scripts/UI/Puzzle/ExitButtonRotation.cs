using UnityEngine;
using System.Collections;

public class ExitButtonRotation : MonoBehaviour {

	Transform playerCam;
	Vector3 lookDirection;

	// Use this for initialization
	void Start () {
		playerCam = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (playerCam.position);
	}
}
