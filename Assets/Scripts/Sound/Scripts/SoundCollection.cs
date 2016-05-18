using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class SoundCollection : ScriptableObject
{
    public SoundType soundType = SoundType.None;

	public List<Sound> sounds = new List<Sound> ();

    /// <summary>
    /// Return sound of given ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
	public Sound Find (int id)
	{
		return sounds.FirstOrDefault (s => s.id == id);
	}

    /// <summary>
    /// Return sound of given name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
	public Sound Find (string name)
	{
		return sounds.FirstOrDefault (s => s.name == name);
	}

    /// <summary>
    /// Return sound of given audioclip
    /// </summary>
    /// <param name="audioClip"></param>
    /// <returns></returns>
    public Sound Find(AudioClip audioClip)
    {
        return sounds.FirstOrDefault(s => s.audioClip == audioClip);
    }

    /// <summary>
    /// Add sound to list of cached sounds
    /// </summary>
    /// <param name="sound"></param>
    public void Add (Sound sound)
	{
        if (!sounds.Contains(sound))
        {
            sound.type = soundType;
            sounds.Add(sound);
        }
	}

    /// <summary>
    /// Add new empty sound to list of cached sounds
    /// </summary>
    public void Add()
    {
        Sound newSound = new Sound();
        newSound.type = soundType;
        newSound.show = true;
        sounds.Add(newSound);
    }

    /// <summary>
    /// Remove sound from list of cached sounds
    /// </summary>
    /// <param name="sound"></param>
	public void Remove (Sound sound)
	{
		sounds.Remove (sound);
	}

    /// <summary>
    /// Reset sound 
    /// </summary>
    /// <param name="sound"></param>
    public void Reset(Sound sound)
    {
        for(int i = 0; i < sounds.Count; i++)
        {
            if(sounds[i] == sound)
            {
                Sound newSound = new Sound();
                newSound.type = soundType;
                newSound.show = true;
                sounds[i] = newSound;
                break;
            }
        }
    }

    /// <summary>
    /// Sort sounds by name
    /// </summary>
	public void Sort ()
	{
		sounds.Sort ((a, b) => { return (a.type + a.Name).CompareTo ((b.type + b.Name)); });
	}

    /// <summary>
    /// Clear all sounds
    /// </summary>
	[ContextMenu ("Clear")]
	public void Clear ()
	{
		sounds.Clear ();
	}

    /// <summary>
    /// Return list of all sound IDs
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
	public List<int> GetSoundIds (SoundType type=SoundType.Any)
	{
		List<int> soundIds = new List<int> ();
		sounds.ForEach (s => { if (type.Intersects (s.type)) soundIds.Add (s.id); });

		return soundIds;
	}

    /// <summary>
    /// Return list of all sound names
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
	public List<string> GetSoundNames (SoundType type=SoundType.Any)
	{
		List<string> soundNames = new List<string> ();
		sounds.ForEach (s => { if (type.Intersects (s.type)) soundNames.Add (s.Name); });
			
		return soundNames;
	}
}