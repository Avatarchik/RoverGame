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
	public Text titleText;
	public Text countText;
	int startFontSize;
	public Ingredient myIngredient;

	// Use this for initialization
	void Start () {
		startFontSize = titleText.fontSize;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetWireCount (int count) {
		countText.text = count.ToString ();
	}

	public void SetTextSizeUp(){
		titleText.fontSize = 45;
	}

	public void SetTextSizeDown(){
		titleText.fontSize = startFontSize;
	}
}
