using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio;

namespace Sol
{
    public class AudioPanel : MonoBehaviour
    {
        public GameObject root;

		public AudioMixer masterMixer;
		public float masterStartVol;
		public float effectsStartVol;
		public float voicesStartVol;
		public float musicStartVol;

        public Slider masterslider;
        public Slider musicSlider;
        public Slider soundEffectsSlider;
        public Slider voiceSlider;

		void Start() {
			masterStartVol = GetStartVolume ("masterVol");
			effectsStartVol = GetStartVolume ("effectsVol");
			voicesStartVol = GetStartVolume ("voicesVol");
			musicStartVol = GetStartVolume ("musicVol");
		}

		public float GetStartVolume (string parameter) {
			float value;
			bool result = masterMixer.GetFloat (parameter, out value);
			if (result) {
				return value;
			} else {
				return 0.0f;
			}
		}

        public void Activate()
        {
            root.SetActive(true);
        }


        public void Deactivate()
        {
            root.SetActive(false);
        }

        //TODO implement way for audio to actually be managed ;-;
        private void SetMasterSlider(float f)
        {
			float newVolume = Mathf.Lerp (-50.0f, masterStartVol, f);
			masterMixer.SetFloat ("masterVol", newVolume);
        }


        private void SetMusicSlider(float f)
        {
			float newVolume = Mathf.Lerp (-50.0f, musicStartVol, f);
			masterMixer.SetFloat ("musicVol", newVolume);
        }


        private void SetSoundFffectsSlider(float f)
        {
			float newVolume = Mathf.Lerp (-50.0f, effectsStartVol, f);
			masterMixer.SetFloat ("effectsVol", newVolume);
        }


        private void SetVoiceSlider(float f)
        {
			float newVolume = Mathf.Lerp (-50.0f, voicesStartVol, f);
			masterMixer.SetFloat ("voicesVol", newVolume);
        }


        private void Awake()
        {
            masterslider.onValueChanged.AddListener(SetMasterSlider);
            musicSlider.onValueChanged.AddListener(SetMusicSlider);
            soundEffectsSlider.onValueChanged.AddListener(SetSoundFffectsSlider);
            voiceSlider.onValueChanged.AddListener(SetVoiceSlider);
        }
    }
}

