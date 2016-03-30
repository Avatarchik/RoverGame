using UnityEngine;
using System.Collections;

///Developed by Indie Games Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268

[DisallowMultipleComponent]
public class Destroy : MonoBehaviour
{
		/// <summary>
		/// Destroy time.
		/// </summary>
		public float time;

		// Use this for initialization
		void Start ()
		{
				///Destry the current gameobject
				Destroy (gameObject, time);
		}
}
