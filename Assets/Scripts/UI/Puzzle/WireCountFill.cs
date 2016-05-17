using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WireCountFill : MonoBehaviour {

	public enum WireType
	{
		Aluminum,
		Copper,
		Gold,
		Silver
	}
	public WireType myWireType;
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
	public int initialWireCount;
	/// <summary>
	/// What is this wire's current inventory count?
	/// </summary>
	public int currentWireCount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateWireCount(int invCount){
		fillImage.fillAmount = invCount / initialWireCount;

	}
}
