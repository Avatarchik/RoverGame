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
	public Image wireImage;
	int startFontSize;
	int startCountSize;
	Color startTitleColor;
	Color startWireColor;
	Color startCountColor;
	Color highlightTitleColor;
	public Color highlightWireColor;
	public Color highlightCountColor;
	public Ingredient myIngredient;

	// Use this for initialization
	void Start () {
		startFontSize = titleText.fontSize;
		startCountSize = countText.fontSize;
		startTitleColor = titleText.color;
		startWireColor = wireImage.color;
		startCountColor = countText.color;
		highlightTitleColor = new Color (startTitleColor.r, startTitleColor.g, startTitleColor.b, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetWireCount (int count) {
		countText.text = count.ToString ();
	}

	public void SetTextSizeUp(){
		titleText.fontSize = 32;
		countText.fontSize = 37;
		titleText.color = highlightTitleColor;
		wireImage.color = highlightWireColor;
		countText.color = highlightCountColor;
	}

	public void SetTextSizeDown(){
		titleText.fontSize = startFontSize;
		countText.fontSize = startCountSize;
		titleText.color = startTitleColor;
		wireImage.color = startWireColor;
		countText.color = startCountColor;
	}
}
