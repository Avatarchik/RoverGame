using UnityEngine;
using System.Collections;

namespace Sol
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundSource : MonoBehaviour
    {
        public System.Action<SoundSource> PlayEvent = delegate { };
        public System.Action<SoundSource> StopEvent = delegate { };

        [SoundIdAttribute]
        public int soundId;
        public bool playOnEnable = false;

        [HideInInspector]
        public AudioSource cachedAudioSource;
        private bool isPlaying = false;
		private Sound currentSound;


        public void Play()
        {
            Play(GameManager.Get<SoundManager>().Find(soundId));
        }


        public void Play(Sound sound, float fadeTime = 0f)
        {
            StopAllCoroutines();
            CachedAudioSource.Stop();

            CurrentSound = sound;
            Volume = fadeTime == 0f ? VolumeFromSettings : 0f;

            CachedAudioSource.clip = sound.audioClip;
            CachedAudioSource.loop = sound.loop;
            CachedAudioSource.pitch = Random.Range(sound.minPitch, sound.maxPitch);
            CachedAudioSource.spatialBlend = sound.spatialBlend;
            CachedAudioSource.volume = sound.volume;
            CachedAudioSource.loop = sound.loop;

            CachedAudioSource.Play();

            //TODO dont relly on this
            CachedAudioSource.spatialBlend = 0;

            if (fadeTime > 0f) SetVolumeTo(VolumeFromSettings, fadeTime);

            isPlaying = true;
            PlayEvent(this);
        }


        public void Stop(float fadeTime = 0f)
        {
            StopAllCoroutines();

            if (fadeTime > 0)
            {
                SetVolumeTo(0f, fadeTime, delegate ()
                {
                    CachedAudioSource.Stop();
                    CachedAudioSource.clip = null;
                });
            }
            else
            {
                CachedAudioSource.Stop();
                CachedAudioSource.clip = null;
            }

            isPlaying = false;
            StopEvent(this);

            //TODO get recycling of sounds working
            Destroy(gameObject);
            //ObjectPool.Recycle(gameObject);
        }


        public void SetVolumeTo(float volume, float fadeTime, System.Action callback = null)
        {
            StopAllCoroutines();
            StartCoroutine(ChangeVolumeCoroutine(0, volume, fadeTime, callback));
        }


        public float Volume
        {
            get { return CachedAudioSource.volume; }
            set { CachedAudioSource.volume = value; }
        }


        public SoundType Type
        {
            get { return CurrentSound != null ? CurrentSound.type : SoundType.None; }
        }


        public bool IsPlaying
        {
            get { return isPlaying && CachedAudioSource.isPlaying; }
        }


        public int CurrentSoundId
        {
            get { return CurrentSound != null ? CurrentSound.id : -1; }
        }


        private Sound CurrentSound
        {
            get { return currentSound; }
            set
            {
                currentSound = value;

                SoundSettings.EffectsVolumeChanged -= OnVolumeChanged;
                SoundSettings.MusicVolumeChanged -= OnVolumeChanged;
                SoundSettings.SpeechVolumeChanged -= OnVolumeChanged;

                switch (Type)
                {
                    case SoundType.Effect:
                        SoundSettings.EffectsVolumeChanged += OnVolumeChanged;
                        break;

                    case SoundType.Music:
                        SoundSettings.MusicVolumeChanged += OnVolumeChanged;
                        break;

                    case SoundType.Speech:
                        SoundSettings.SpeechVolumeChanged += OnVolumeChanged;
                        break;
                }
            }
        }


        private AudioSource CachedAudioSource
        {
            get { return cachedAudioSource ? cachedAudioSource : (cachedAudioSource = GetComponent<AudioSource>()); }
        }


        private float VolumeFromSettings
        {
            get
            {
                switch (Type)
                {
                    case SoundType.Effect: return SoundSettings.EffectsVolume;
                    case SoundType.Music: return SoundSettings.MusicVolume;
                    case SoundType.Speech: return SoundSettings.SpeechVolume;
                    default: return 0f;
                }
            }
        }


        private IEnumerator ChangeVolumeCoroutine(float from, float to, float time, System.Action callback = null)
        {
            float elapsedTime = 0f;

            while (elapsedTime < time)
            {
                yield return null;
                elapsedTime += Time.deltaTime;
                Volume = Mathf.Lerp(from, to, Mathf.Clamp01(elapsedTime / time));
            }

            if (callback != null) callback();
        }


        private void OnVolumeChanged(float volume)
        {
            Volume = VolumeFromSettings;
        }


        private void OnEnable()
        {
            if (playOnEnable) Play();
        }


        private void Update()
        {
            if (isPlaying && !CachedAudioSource.isPlaying) Stop();
        }
    }
}