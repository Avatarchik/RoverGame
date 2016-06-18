using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
	public class WaypointGroup : MonoBehaviour 
	{
		public Transform waypointObject;
		public List<GameObject> shitToCheck = new List<GameObject>();


		private IEnumerator CheckShit()
		{
			foreach (GameObject go in shitToCheck) 
			{
				if (go != null)
					goto Restart;
			}
			Destroy (waypointObject.gameObject);
			yield return null;

			Restart:
			yield return new WaitForSeconds (2f);
			StartCoroutine (CheckShit ());
		}


		private void Awake()
		{
			StartCoroutine (CheckShit ());
		}
	}
}