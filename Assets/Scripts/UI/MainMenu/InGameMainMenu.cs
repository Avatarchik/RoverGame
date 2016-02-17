using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class InGameMainMenu : Menu 
{
	public Button openInventoryButton;
	public Button openLogFilesButton;
	public Button openMapButton;
	public Button openOptionsButton;
	public Button openLoadSaveButton;
	public Button openQuitGameButton;
	public Button openCloseButton;



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

	public void OpenMapButton()
	{
		Debug.Log ("Open Map Button was pressed");
		/* Wait for functionality */
		//UIManager.Open<Map> ();
	}

	public void OpenOptionsButton()
	{
		Debug.Log ("Open Options Button was pressed");
		UIManager.Open<OptionsMenu> ();
	}

	public void OpenLoadSaveButton()
	{
		Debug.Log ("Open Load Save Button was pressed");
		/* Wait for functionality */
		//UIManager.Open<LoadSave> ();
	}

	public void OpenQuitGameButton()
	{
		Debug.Log ("Quit Game Button was pressed");
		Application.Quit ();
	}

	public void OpenCloseButton()
	{	
		/* This isn't actually working, so I'm calling the wrong function apparently, also throws warnings when pressed! */
		Debug.Log ("Close Menu Button was pressed");
		base.Close();
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{

		/* Logic is wrong, need to be fixed - and/or this isn't in the appropriate class?
		if (Input.GetKeyDown (KeyCode.Escape) && !isActive) {
			base.Open ();
		} else
			base.Close ();
		*/

	}

	private void Awake()
	{
		openInventoryButton.onClick.AddListener (OpenInventoryButton);
		openLogFilesButton.onClick.AddListener (OpenLogFilesButton);
		openMapButton.onClick.AddListener (OpenMapButton);
		openOptionsButton.onClick.AddListener (OpenOptionsButton);
		openLoadSaveButton.onClick.AddListener(OpenLoadSaveButton);
		openQuitGameButton.onClick.AddListener (OpenQuitGameButton);

}

}
