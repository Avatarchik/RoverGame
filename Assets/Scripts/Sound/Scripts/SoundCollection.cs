using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class SoundCollection : ScriptableObject
{
	public List<Sound> sounds = new List<Sound> ();

	public Sound Find (int id)
	{
		return sounds.FirstOrDefault (s => s.id == id);
	}


	public Sound Find (string name)
	{
		return sounds.FirstOrDefault (s => s.name == name);
	}


	public void Add (Sound sound)
	{
		if (!sounds.Contains (sound)) sounds.Add (sound);
	}


	public void Remove (Sound sound)
	{
		sounds.Remove (sound);
	}


	public void Sort ()
	{
		sounds.Sort ((a, b) => { return (a.type + a.Name).CompareTo ((b.type + b.Name)); });
	}


	[ContextMenu ("Clear")]
	public void Clear ()
	{
		sounds.Clear ();
	}


	public List<int> GetSoundIds (SoundType type=SoundType.Any)
	{
		List<int> soundIds = new List<int> ();
		sounds.ForEach (s => { if (type.Intersects (s.type)) soundIds.Add (s.id); });

		return soundIds;
	}


	public List<string> GetSoundNames (SoundType type=SoundType.Any)
	{
		List<string> soundNames = new List<string> ();
		sounds.ForEach (s => { if (type.Intersects (s.type)) soundNames.Add (s.Name); });
			
		return soundNames;
	}
}
