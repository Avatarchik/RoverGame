using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class AudioPanel : MonoBehaviour
    {
        public GameObject root;

        public Slider masterslider;
        public Slider musicSlider;
        public Slider soundEffectsSlider;
        public Slider voiceSlider;


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

        }


        private void SetMusicSlider(float f)
        {

        }


        private void SetSoundFffectsSlider(float f)
        {

        }


        private void SetVoiceSlider(float f)
        {

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

