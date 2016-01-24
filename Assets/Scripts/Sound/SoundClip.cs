using UnityEngine;
using System.Collections;

[System.Serializable]
public class SoundClip : MonoBehaviour
{
    public int id = -1;
    public string soundName = "";
    public SoundType type = SoundType.None;
    public AudioClip clip;
    public int instanceLimit = 3;
    public bool loop = false;
    public float minPitch = 1f;
    public float maxPitch = 1f;
    public float spatialBlend = 0f;


	public SoundClip()
    {
        this.id = System.Guid.NewGuid().GetHashCode();
    }


    public SoundClip (AudioClip clip, SoundType type)
    {
        this.id = System.Guid.NewGuid().GetHashCode();
        this.instanceLimit = 3;
        this.clip = clip;
        this.type = type;
        this.soundName = clip.name;
        this.loop = type == SoundType.Music;
    }


    public string SoundName
    {
        get { return !string.IsNullOrEmpty(soundName) ? soundName : (clip ? clip.name : id.ToString()); }
    }
}
