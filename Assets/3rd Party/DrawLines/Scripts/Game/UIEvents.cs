using UnityEngine;
using System.Collections;
using UnityEngine.UI;

///Developed by Indie Games Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268

[DisallowMultipleComponent]
public class UIEvents : MonoBehaviour
{
		public void ChangeMusicLevel (Slider slider)
		{
				if (slider == null) {
						return;
				}
				GameObject.Find ("AudioSources").GetComponents<AudioSource> () [0].volume = slider.value;
		}

		public void ChangeEffectsLevel (Slider slider)
		{
				if (slider == null) {
						return;
				}
				GameObject.Find ("AudioSources").GetComponents<AudioSource> () [1].volume = slider.value;
		}

		public void ShowResetGameConfirmDialog ()
		{
				GameObject.Find ("ResetGameConfirmDialog").GetComponent<ConfirmDialog> ().Show ();
		}

		public void ShowExitConfirmDialog ()
		{
				GameObject.Find ("ExitConfirmDialog").GetComponent<ConfirmDialog> ().Show ();
		}

		public void ResetGameConfirmDialogEvent (GameObject value)
		{
				if (value == null) {
						return;
				}

				if (value.name.Equals ("YesButton")) {
						Debug.Log ("Reset Game Confirm Dialog : No button clicked");
						DataManager.instance.ResetGameData ();
				} else if (value.name.Equals ("NoButton")) {
						Debug.Log ("Reset Game Confirm Dialog : No button clicked");
				}
				GameObject.Find ("ResetGameConfirmDialog").GetComponent<ConfirmDialog> ().Hide ();
		}

		public void ExitConfirmDialogEvent (GameObject value)
		{
				if (value.name.Equals ("YesButton")) {
						Debug.Log ("Exit Confirm Dialog : Yes button clicked");
						Application.Quit ();
				} else if (value.name.Equals ("NoButton")) {
						Debug.Log ("Exit Confirm Dialog : No button clicked");
				}
				GameObject.Find ("ExitConfirmDialog").GetComponent<ConfirmDialog> ().Hide ();
		}

		public	void MissionButtonEvent (Object value)
		{
				if (value == null) {
						Debug.Log ("Event parameter value is undefined");
						return;
				}

				GameObject missionGameObject = (GameObject)value;
				Mission.wantedMission = missionGameObject.GetComponent<Mission> ();

				//LoadLevelsScene ();
		}

		public void LevelButtonEvent (Object value)
		{
				if (value == null) {
						Debug.Log ("Event parameter value is undefined");
						return;
				}

				GameObject levelGameObject = (GameObject)value;
				TableLevel.wantedLevel = levelGameObject.GetComponent<TableLevel> ();
				LevelsTable.currentLevelID = TableLevel.wantedLevel.ID;

                //LoadGameScene ();
		}

		public	void GameNextButtonEvent ()
		{
				GameObject.Find ("GameScene").GetComponent<PuzzleManager> ().NextLevel ();
		}

		public void GameBackButtonEvent ()
		{
				GameObject.Find ("GameScene").GetComponent<PuzzleManager> ().PreviousLevel ();
		}

		public void GameRefreshButtonEvent ()
		{
				GameObject.Find ("GameScene").GetComponent<PuzzleManager> ().RefreshGrid ();
		}

		public void AwesomeDialogNextButtonEvent ()
		{
				if (LevelsTable.currentLevelID == LevelsTable.tableLevels.Count) {
						LoadLevelsScene ();
						return;
				}
				BlackArea.Hide ();
				GameObject.FindObjectOfType<AwesomeDialog> ().Hide ();
				GameObject.Find ("GameScene").GetComponent<PuzzleManager> ().NextLevel ();
		}

		public void LoadMainScene ()
		{
				StartCoroutine (LoadSceneAsync ("Main"));
		}

		public void LoadHowToPlayScene ()
		{
				StartCoroutine (LoadSceneAsync ("HowToPlay"));
		}

		public void LoadMissionsScene ()
		{
				StartCoroutine (LoadSceneAsync ("Missions"));
		}

		public void LoadOptionsScene ()
		{
				StartCoroutine (LoadSceneAsync ("Options"));
		}

		public void LoadLevelsScene ()
		{
				StartCoroutine (LoadSceneAsync ("Levels"));
		}

		public void LoadGameScene ()
		{
				StartCoroutine (LoadSceneAsync ("Game"));
		}

		/// <summary>
		/// Loads the scene Async.
		/// </summary>
		IEnumerator LoadSceneAsync (string sceneName)
		{
			if (!string.IsNullOrEmpty (sceneName)) {
				#if UNITY_PRO_LICENSE
					AsyncOperation async = Application.LoadLevelAsync (sceneName);
					while (!async.isDone) {
						yield return 0;
					}
				#else
					Application.LoadLevel (sceneName);
					yield return 0;
				#endif
				}
		}
}