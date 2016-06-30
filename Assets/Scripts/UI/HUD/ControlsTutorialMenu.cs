using UnityEngine;
using System.Collections;

namespace Sol
{
	public class ControlsTutorialMenu : Menu
	{
		private IEnumerator RunTutorial()
		{
			yield return new WaitForSeconds (12f);
			Open ();
			yield return new WaitForSeconds (6f);
			Close ();
		}


		private void Awake()
		{
			StartCoroutine (RunTutorial ());
		}
	}
}

