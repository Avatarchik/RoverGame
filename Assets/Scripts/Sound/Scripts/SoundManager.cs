using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Audio;

namespace Sol
{
    public class SoundManager : MonoBehaviour
    {
        public SoundSource soundSourcePrefab;
        public List<SoundCollection> collections = new List<SoundCollection>();

        private List<SoundSource> sources = new List<SoundSource>();
		public AudioMixer masterMixer;


        /// <summary>
        /// Play sound source of given ID
        /// </summary>
        /// <param name="soundId"></param>
        /// <param name="fadeTime"></param>
        /// <param name="parent"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public SoundSource Play(int soundId, float fadeTime = 0f, Transform parent = null, Vector3 position = default(Vector3))
        {
            return Play(Find(soundId), fadeTime, parent, position);
        }

        /// <summary>
        /// Play sound source of given sound name
        /// </summary>
        /// <param name="soundName"></param>
        /// <param name="fadeTime"></param>
        /// <param name="parent"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public SoundSource Play(string soundName, float fadeTime = 0f, Transform parent = null, Vector3 position = default(Vector3))
        {
            Debug.Log("playing sound by name");
            return Play(Find(soundName), fadeTime, parent, position);
        }

        /// <summary>
        /// Play sound source of given audio clip
        /// </summary>
        /// <param name="audioClip"></param>
        /// <param name="fadeTime"></param>
        /// <param name="parent"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public SoundSource Play(AudioClip audioClip, float fadeTime = 0f, Transform parent = null, Vector3 position = default(Vector3))
        {
			//Debug.Log("playing sound by audio clip");
            return Play(Find(audioClip), fadeTime, parent, position);
        }

        /// <summary>
        /// Play sound source of given sound
        /// </summary>
        /// <param name="sound"></param>
        /// <param name="fadeTime"></param>
        /// <param name="parent"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public SoundSource Play(Sound sound, float fadeTime = 0f, Transform parent = null, Vector3 position = default(Vector3))
        {
            if (sound == null) return null;

            Initialize(sound, fadeTime);
            SoundSource source = GetSoundSource(sound, parent, position);
            //source.SetVolumeTo(sound.volume, 0);

            if (source)
            {
                source.Play(sound, fadeTime);
            }
            else
            {
                Debug.LogError("source is null!");
            }

            return source;
        }

        /// <summary>
        /// Return whether or not sound  source of given sound is playing
        /// </summary>
        /// <param name="sound"></param>
        /// <returns></returns>
        public bool IsPlaying(Sound sound)
        {
            if (sources.Count == 0) return false;
            return sources.FirstOrDefault(s => s.soundId == sound.id) != null;
        }

        /// <summary>
        /// Return sound of given ID
        /// </summary>
        /// <param name="soundId"></param>
        /// <returns></returns>
        public Sound Find(int soundId)
        {
            Sound sound = null;
            foreach (SoundCollection c in collections)
            {
                sound = c.Find(soundId);
                if (sound != null) break;
            }

            if (sound == null) Debug.LogError(string.Format("Could not find sound with id {0}.", soundId));

            return sound;
        }

        /// <summary>
        /// Return sound of given sound name
        /// </summary>
        /// <param name="soundName"></param>
        /// <returns></returns>
        public Sound Find(string soundName)
        {
            Sound sound = null;
            foreach (SoundCollection c in collections)
            {
                sound = c.Find(soundName);
                if (sound != null) break;
            }

            if (sound == null) Debug.LogError(string.Format("Could not find sound with name {0}.", soundName));

            Debug.Log("finding sound by name");

            return sound;
        }

        /// <summary>
        /// Return sound of given audio clip
        /// </summary>
        /// <param name="audioClip"></param>
        /// <returns></returns>
        public Sound Find(AudioClip audioClip)
        {
            Sound sound = null;
            foreach (SoundCollection c in collections)
            {
                sound = c.Find(audioClip);
                if (sound != null) break;
            }

            if (sound == null) Debug.Log(string.Format("Could not find sound with name {0}.", audioClip.name));

            return sound;
        }

        /// <summary>
        /// Stop playing sound of given sound
        /// </summary>
        /// <param name="sound"></param>
        public void Stop(Sound sound)
        {
            foreach (SoundSource soundSource in sources)
            {
                if (soundSource.CurrentSoundId == sound.id)
                {
                    soundSource.Stop();
                }
            }
        }

        /// <summary>
        /// Stop sound of given ID
        /// </summary>
        /// <param name="soundId"></param>
        public void Stop(int soundId)
        {
            for (int i = sources.Count - 1; i >= 0; i--)
            {
                if (sources[i].CurrentSoundId == soundId)
                {
                    Stop(sources[i]);
                }
            }
        }

        /// <summary>
        /// Stop sound of given sound source
        /// </summary>
        /// <param name="soundSource"></param>
        public void Stop(SoundSource soundSource)
        {
            soundSource.Stop();
        }

        /// <summary>
        /// clear out sound sources from the obect pool
        /// </summary>
        public void Reset()
        {
            ObjectPool.Clear(soundSourcePrefab.gameObject);
        }

        /// <summary>
        /// Cleanup sound source
        /// </summary>
        /// <param name="source"></param>
        private void OnSoundSourceStop(SoundSource source)
        {
            source.StopEvent -= OnSoundSourceStop;
            sources.Remove(source);
            ObjectPool.Recycle(source.gameObject);
        }

        /// <summary>
        /// Initialize sound
        /// </summary>
        /// <param name="sound"></param>
        /// <param name="fadeTime"></param>
        private void Initialize(Sound sound, float fadeTime)
        {
            switch (sound.type)
            {
                case SoundType.Effect:
                case SoundType.Music:
                case SoundType.Speech:
                    sources.FindAll(s => { return s.CurrentSoundId != sound.id && s.Type == sound.type && s.IsPlaying; }).ForEach(s => s.Stop(fadeTime));
                    break;
            }
        }

        /// <summary>
        /// Get sound source from collections
        /// </summary>
        /// <param name="sound"></param>
        /// <param name="parent"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private SoundSource GetSoundSource(Sound sound, Transform parent, Vector3 position)
        {
            SoundSource source = null;
            if (!parent) parent = transform;

            switch (sound.type)
            {
			case SoundType.Effect:
                case SoundType.Music:
                case SoundType.Speech:
				if (sources.FindAll(s => { return s.name == sound.name; }).Count <= sound.instanceLimit)
                    {
                        source = ObjectPool.Request<SoundSource>(soundSourcePrefab, parent, position, Quaternion.identity);
                        source.StopEvent += OnSoundSourceStop;
                    }
                    break;
            }

            if (source) sources.Add(source);
            return source;
        }
    }
}