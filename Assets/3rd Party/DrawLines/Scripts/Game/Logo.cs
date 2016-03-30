using UnityEngine;
using System.Collections;

///Developed by Indie Games Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
 
public class Logo : MonoBehaviour {

	public float sleepTime = 5;
	// Use this for initialization
	void Start () {
		Invoke ("LoadMainScene", sleepTime);
	}

	private void LoadMainScene(){
		Application.LoadLevel ("Main");
	}
	
}
