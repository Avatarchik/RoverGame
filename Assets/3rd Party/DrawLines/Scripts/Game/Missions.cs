using UnityEngine;
using UnityEngine.UI;
using System.Collections;

///Developed by Indie Games Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268

[RequireComponent(typeof(MissionCreator))]
[DisallowMultipleComponent]
public class Missions : MonoBehaviour
{
		/// <summary>
		/// Whether to enable the lock feature for or not.
		/// </summary>
		public bool enableLockFeature = true;

		// Use this for initialization
		void Awake ()
		{
				DataManager.instance.InitGameData ();

				if (enableLockFeature) {
						SetLocks ();
				} else {
						RemoveLocks ();
				}
		}

		/// <summary>
		/// Sets the locks for missions.
		/// </summary>
		private void SetLocks ()
		{
				Mission [] missions = GameObject.FindObjectsOfType<Mission> ();

				Mission mission = null;
				foreach (DataManager.MissionData md in DataManager.instance.filterdMissionsData) {
						mission = FindMissionById (md.ID, missions);
						if (mission != null) {
								mission.isLocked = md.isLocked;
								if (md.isLocked) {
										mission.GetComponent<Button> ().interactable = false;
								} else {
										Transform lockGameObject = mission.transform.Find ("Lock");
										if (lockGameObject != null) {
												Image lockImage = lockGameObject.GetComponent<Image> ();
												lockImage.enabled = false;
										}
								}
						}
				}
		}

		/// <summary>
		/// Removes the locks for missions.
		/// </summary>
		private void RemoveLocks ()
		{
				Mission [] missions = GameObject.FindObjectsOfType<Mission> ();
				foreach (Mission mission in missions) {
						mission.isLocked = false;
						Transform lockGameObject = mission.transform.Find ("Lock");
						if (lockGameObject != null) {
								Image lockImage = lockGameObject.GetComponent<Image> ();
								lockImage.enabled = false;
						}
				}
		}
	
		/// <summary>
		/// Find the mission component by ID.
		/// </summary>
		/// <returns>The mission component by id.</returns>
		/// <param name="ID">ID of mission.</param>
		/// <param name="missions">Missions Components.</param>
		public Mission FindMissionById (int ID, Mission[] missions)
		{
				if (missions == null) {
						return null;
				}

				foreach (Mission mission in missions) {
						if (mission.ID == ID) {
								return mission;
						}
				}
				return null;
		}
}
