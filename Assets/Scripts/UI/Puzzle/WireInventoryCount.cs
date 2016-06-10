using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WireInventoryCount : MonoBehaviour {

	public enum WireType {
		Aluminum,
		Copper,
		Silver,
		Gold
	}

	public WireType myWireType;
	Text countText;

	// Use this for initialization
	void Start () {
		countText = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetWireCount (int count) {
		countText.text = count.ToString ();
	}
}
