using UnityEngine;
using System.Collections;

public class TrailRendererLayering : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<TrailRenderer> ().sortingLayerName = "PuzzleForeground";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
