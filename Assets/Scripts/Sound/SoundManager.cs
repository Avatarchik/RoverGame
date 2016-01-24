using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SoundManager : MonoBehaviour
{
    public Sound soundPrefab;
    public List<SoundClip> soundclips = new List<SoundClip> ();

    private List<Sound> sources = new List<Sound>();


    public SoundClip Play(int soundId, Transform parent=null, Vector3 position = default(Vector3))
    {
        return Play(Find(soundId));
    }


    public SoundClip Play(SoundClip soundclip, Transform parent=null, Vector3 position = default(Vector3))
    {
        if (soundclip == null) return null;

        Initialize(soundclip);
        return soundclip;
    }


    public SoundClip Find (int soundId)
    {
        SoundClip soundClip = null;
        foreach (SoundClip sc in soundclips)
        {
            if (sc.id == soundId)
            {
                soundClip = sc;
                break;
            }
        }

        if (soundClip == null) Debug.LogError(string.Format("Could not find sound with id {0}", soundId));

        return soundClip;
    }


    private void Initialize(SoundClip soundclip)
    {
        switch(soundclip.type)
        {
            case SoundType.Music:
                sources.FindAll(s => { return s.CurrentSoundClip.id != soundclip.id && s.CurrentSoundClip.type == soundclip.type && s.IsPlaying; }).ForEach(s => s.Stop());
                break;
        }
    }


    private Sound GetSound (Sound sound, Transform parent, Vector3 position)
    {
        Sound source = null;
        if (!parent) parent = transform;

        switch (sound.CurrentSoundClip.type)
        {
            case SoundType.Effect:
            case SoundType.Speech:
                if (sources.FindAll(s => { return s.CurrentSoundClip == sound.CurrentSoundClip; }).Count > 0)
                {
                    source = sources.FirstOrDefault(s => s.CurrentSoundClip.type == sound.CurrentSoundClip.type && !s.IsPlaying);
                    if (!source) source = ObjectPool.Request<Sound>(soundPrefab, parent, position, Quaternion.identity);
                }
                break;

            case SoundType.Music:
                if (sources.FindAll(s => { return s.CurrentSoundClip == sound.CurrentSoundClip; }).Count > 0)
                {
                    source = sources.FirstOrDefault(s => s.CurrentSoundClip.type == sound.CurrentSoundClip.type && !s.IsPlaying);
                    if (!source) source = ObjectPool.Request<Sound>(soundPrefab, parent, position, Quaternion.identity);
                }
                break;
        }

        if (source) sources.Add(source);

        return source;
    }
}
