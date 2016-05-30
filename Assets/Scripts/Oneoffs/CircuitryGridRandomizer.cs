using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CircuitryGridRandomizer : MonoBehaviour {

	public Color circuitColor;
	public Sprite[] circuitSprites;

	// Use this for initialization
	void Start () {
		RandomizeGridSprites ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void RandomizeGridSprites() {
		foreach (Transform circuit in transform) {
			circuit.GetComponent<Image> ().sprite = circuitSprites [Mathf.RoundToInt (UnityEngine.Random.Range (0, circuitSprites.Length))];
			circuit.GetComponent<Image> ().color = circuitColor;
		}
	}
}
