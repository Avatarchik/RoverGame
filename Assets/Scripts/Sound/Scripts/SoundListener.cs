using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace WisdomTools.Common
{
	[RequireComponent (typeof (AudioListener))]
	public class SoundListener : MonoBehaviour
	{
		[Tooltip ("Lowest priority value will be the active AudioListener in a scene.")]
		public int priority = 1;

		private AudioListener cachedAudioListener;

		protected static bool doPriorityCheck = true;
		protected static List<SoundListener> listeners = new List<SoundListener> ();


		protected bool IsEnabled
		{
			get { return CachedAudioListener.enabled; }
			set
			{
				if (IsEnabled != value)
				{
					CachedAudioListener.enabled = value;
				}
			}
		}


		protected SoundListener EnabledListener
		{
			set { listeners.ForEach (l => l.IsEnabled = (l == value)); }
		}


		private AudioListener CachedAudioListener
		{
			get { return cachedAudioListener ? cachedAudioListener : (cachedAudioListener = GetComponent<AudioListener> ()); }
		}


		public void Reevaluate()
		{
			doPriorityCheck = true;
		}


		public void SetAsPriority()
		{
			int lowestPriority = priority;

			foreach(SoundListener sl in listeners)
			{
				if(sl.priority <= lowestPriority)
				{
					lowestPriority = sl.priority - 1;
				}
			}

			priority = lowestPriority;

			Reevaluate();
		}


		private void OnEnable ()
		{
			if (!listeners.Contains (this))
			{
				listeners.Add (this);
				doPriorityCheck = true;
			}
		}


		private void OnDisable ()
		{
			listeners.Remove (this);
			doPriorityCheck = true;
		}


		protected virtual void Update ()
		{
			if (doPriorityCheck && listeners.Count > 0)
			{
				doPriorityCheck = false;
				listeners.Sort ((a, b) => a.priority.CompareTo (b.priority));
				EnabledListener = listeners[0];
			}
		}


		private void OnDrawGizmos ()
		{
			if (IsEnabled)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawWireSphere (transform.position, 2f);
			}
		}
	}
}
