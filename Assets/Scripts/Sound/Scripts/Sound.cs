using UnityEngine;
using System.Collections;



[System.Serializable]
public class Sound
{
	public int id = -1;
	public string name = "";
	public SoundType type = SoundType.None;
	public AudioClip audioClip;
	public int instanceLimit = 3;
	public bool loop = false;
	public float minPitch = 1f;
	public float maxPitch = 1f;
	public float spatialBlend = 1f;
    public float volume = 1f;

    [HideInInspector]
    public bool show = false;
		
		
	public Sound()
	{
		this.id = System.Guid.NewGuid ().GetHashCode ();
	}


	public Sound (AudioClip clip, SoundType type)
	{
		this.id = System.Guid.NewGuid ().GetHashCode ();
		this.instanceLimit = 3;
		this.audioClip = clip;
		this.type = type;
		this.name = clip.name;
		this.loop = type == SoundType.Music;
	}


	public string Name
	{
		get { return !string.IsNullOrEmpty (name) ? name : (audioClip ? audioClip.name : id.ToString ()); }
	}
}
