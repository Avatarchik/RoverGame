using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class InGameMainMenu : Menu 
{
	public Button openMapButton;
	public Button openCraftingButton;
	public Button openInventoryButton;
	public Button openLogFilesButton;
	public Button openSystemButton;

	public void OpenMapButton()
	{
		Debug.Log ("Open Map Button was pressed");
		/* Wait for functionality */
		//UIManager.Open<Map> ();
	}

	public void OpenCraftingButton()
	{
		Debug.Log ("Open Craft Button was pressed");
		/* Wait for functionality */
		UIManager.Open<Crafting> ();
	}

	public void OpenInventoryButton()
	{
		Debug.Log ("Inventory Button was pressed");
		UIManager.Open<Inventory> ();
	}

	public void OpenLogFilesButton()
	{
		Debug.Log ("Open Log Files Button was pressed");
		/* Wait for namespace to be created elsewhere */
		//UIManager.Open<Log Files> ();
	}

	public void OpenSystemButton()
	{
		Debug.Log ("Open Log Files Button was pressed");
		/* Wait for namespace to be created elsewhere */
		//UIManager.Open<Log Files> ();
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		openBase ();
	}

	public void openBase()
	{
		// Logic is wrong, need to be fixed - and/or this isn't in the appropriate class?
		if (Input.GetKeyDown (KeyCode.Escape) && !isActive) {
			base.Open();
		} else if (Input.GetKeyDown (KeyCode.Escape) && isActive) {
			base.Close ();
		}
	}

	private void Awake()
	{
		openMapButton.onClick.AddListener (OpenMapButton);
		openCraftingButton.onClick.AddListener (OpenCraftingButton);
		openInventoryButton.onClick.AddListener (OpenInventoryButton);
		openLogFilesButton.onClick.AddListener (OpenLogFilesButton);
		openSystemButton.onClick.AddListener (OpenSystemButton);
}

}
