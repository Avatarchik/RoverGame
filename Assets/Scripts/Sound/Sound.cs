using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Sound : MonoBehaviour
{
    public System.Action<Sound> PlayEvent = delegate { };
    public System.Action<Sound> StopEvent = delegate { };

    public int soundId;
    public bool playOnEnable = false;

    private AudioSource cachedAudioSource;
    private bool isPlaying = false;
    public SoundClip currentSoundClip;


    public SoundType Type
    {
        get { return CurrentSoundClip != null ? CurrentSoundClip.type : SoundType.None; }
    }


    public bool IsPlaying
    {
        get { return isPlaying && cachedAudioSource.isPlaying; }
    }


    public int CurrentSoundId
    {
        get { return CurrentSoundClip != null ? CurrentSoundClip.id : -1; }
    }


    public SoundClip CurrentSoundClip
    {
        get { return currentSoundClip; }
        set
        {
            currentSoundClip = value;

            //TODO inject volume logic here!
            switch(Type)
            {
                case SoundType.Effect:
                    break;

                case SoundType.Music:
                    break;

                case SoundType.Speech:
                    break;
            }
        }
    }


    private AudioSource CachedAudioSource
    {
        get { return cachedAudioSource ? cachedAudioSource : (cachedAudioSource = GetComponent<AudioSource>()); }
    }


    public void Play()
    {
        Play(CurrentSoundClip);
    }


    public void Play(SoundClip soundClip)
    {
        StopAllCoroutines();
        CachedAudioSource.Stop();

        CurrentSoundClip = soundClip;
        CachedAudioSource.clip = CurrentSoundClip.clip;
        CachedAudioSource.Play();
    }


    public void Stop()
    {

    }
}
