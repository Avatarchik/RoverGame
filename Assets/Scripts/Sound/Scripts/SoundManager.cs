using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SoundManager : MonoBehaviour
{
	public SoundSource soundSourcePrefab;
	public List<SoundCollection> collections = new List<SoundCollection> ();

	private List<SoundSource> sources = new List<SoundSource> ();
		
		
	public SoundSource Play (int soundId, float fadeTime=0f, Transform parent=null, Vector3 position=default(Vector3))
	{
		return Play (Find (soundId), fadeTime, parent, position);
	}


	public SoundSource Play (string soundName, float fadeTime=0f, Transform parent=null, Vector3 position=default(Vector3))
	{
		return Play (Find (soundName), fadeTime, parent, position);
	}
		
		
	public SoundSource Play (Sound sound, float fadeTime=0f, Transform parent=null, Vector3 position=default(Vector3))
	{
		if (sound == null) return null;

		Initialize (sound, fadeTime);
		SoundSource source = GetSoundSource (sound, parent, position);

		if (source) source.Play (sound, fadeTime);

		return source;
	}


	public bool IsPlaying (Sound sound)
	{
        if (sources.Count == 0) return false;
		return sources.FirstOrDefault (s => s.soundId == sound.id) != null;
	}


	public Sound Find (int soundId)
	{
		Sound sound = null;
		foreach (SoundCollection c in collections)
		{
			sound = c.Find (soundId);
			if (sound != null) break;
		}

		if (sound == null) Debug.LogError (string.Format ("Could not find sound with id {0}.", soundId));

		return sound;
	}


	public Sound Find (string soundName)
	{
		Sound sound = null;
		foreach (SoundCollection c in collections)
		{
			sound = c.Find (soundName);
			if (sound != null) break;
		}

		if (sound == null) Debug.LogError (string.Format ("Could not find sound with name {0}.", soundName));

		return sound;
	}


    public void Stop(Sound sound)
    {
        foreach (SoundSource soundSource in sources)
        {
            if(soundSource.CurrentSoundId == sound.id)
            {
                soundSource.Stop();
            }
        }
    }


    public void Stop(int soundId)
    {
        for(int i = sources.Count - 1; i >= 0; i--)
        {
            if (sources[i].CurrentSoundId == soundId)
            {
                Stop(sources[i]);
            }
        }
    }


    public void Stop(SoundSource soundSource)
    {
        soundSource.Stop();
    }


    public void Reset ()
	{
		ObjectPool.Clear (soundSourcePrefab.gameObject);
	}


	private void OnSoundSourceStop (SoundSource source)
	{
		source.StopEvent -= OnSoundSourceStop;
		sources.Remove (source);
        ObjectPool.Recycle (source.gameObject);
	}


	private void Initialize (Sound sound, float fadeTime)
	{
		switch (sound.type)
		{
			case SoundType.Music:
				sources.FindAll (s => { return s.CurrentSoundId != sound.id && s.Type == sound.type && s.IsPlaying; }).ForEach (s => s.Stop (fadeTime));
				break;
		}
	}


	private SoundSource GetSoundSource (Sound sound, Transform parent, Vector3 position)
	{
		SoundSource source = null;
		if (!parent) parent = transform;

		switch (sound.type)
		{
			case SoundType.Effect:
            case SoundType.Music:
            case SoundType.Speech:
				if (sources.FindAll (s => { return s.CurrentSoundId == sound.id; }).Count < sound.instanceLimit)
				{
					source = ObjectPool.Request<SoundSource> (soundSourcePrefab, parent, position, Quaternion.identity);
					source.StopEvent += OnSoundSourceStop;
				}
				break;
		}

		if (source) sources.Add (source);

		return source;
	}


    private void Start()
    {
        Play(1010);
    }
}
