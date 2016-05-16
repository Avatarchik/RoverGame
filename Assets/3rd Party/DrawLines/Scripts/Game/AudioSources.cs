using UnityEngine;
using System.Collections;

///Developed by Indie Games Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268

public class AudioSources : MonoBehaviour {

	/// <summary>
	/// The loading canvas instance.
	/// </summary>
	private static AudioSources audioSourcesInstance;
	
	// Use this for initialization
	void Awake ()
	{
		if (audioSourcesInstance == null) {
			audioSourcesInstance = this;
			DontDestroyOnLoad(transform.root.gameObject);
		} else {
			Destroy (gameObject);
		}
	}
}
