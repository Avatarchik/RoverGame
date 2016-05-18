using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WireCountFill : MonoBehaviour {
	/// <summary>
	/// This wire's count text component.
	/// </summary>
	public Text wireCountText;
	/// <summary>
	/// This wire's Fill Image component.
	/// </summary>
	private Image fillImage;
	/// <summary>
	/// This wireType's correlated ingredient
	/// </summary>
	public Ingredient wireIngredient;
	/// <summary>
	/// When the puzzle starts, what is this wire's inventory count?
	/// </summary>
	public float initialWireCount;
	/// <summary>
	/// What is this wire's current inventory count?
	/// </summary>
	public float currentWireCount;
	public float fillRatio;

	// Use this for initialization
	void Start () {
		fillImage = gameObject.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetWireCount(int initialCount){
		initialWireCount = initialCount;
		currentWireCount = initialCount;
		wireCountText.text = initialCount.ToString ();
	}

	public void UpdateWireCount(int invCount){
		currentWireCount = invCount;
		if (initialWireCount > 0) {
			fillRatio = currentWireCount / initialWireCount;
			fillImage.fillAmount = fillRatio;
			wireCountText.text = invCount.ToString ();
		}
	}
}
