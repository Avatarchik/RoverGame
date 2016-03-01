using UnityEngine;
using System.Collections;

namespace Sol
{
    [System.Serializable]
    public class LightSettings
    {
        public float baseIntensity = 1f;
    }

    [System.Serializable]
    public class FlickerSettings
    {
        public float minIntensity = 0f;
        public float maxIntensity = 1f;
        public float minDelay = 0.1f;
        public float maxDelay = 0.5f;
    }


    [System.Serializable]
    public class BounceSettings
    {
        public float minIntensity = 0f;
        public float maxIntensity = 1f;
        public float bounceSpeed = 1f;
    }


    [System.Serializable]
    public class FlashSettings
    {
        public float minIntensity = 0f;
        public float maxIntensity = 1f;
        public float onTime = 1f;
        public float offTime = 1f;
    }

    public class LightController : MonoBehaviour
    {
        public enum LightMode
        {
            On,
            Off,
            Flicker,
            Bounce,
            Flash
        }

        public Light controlledLight;
        public LightMode lightMode = LightMode.On;

        public LightSettings lightSettings;
        public FlickerSettings flickerSettings;
        public BounceSettings bounceSettings;
        public FlashSettings flashSettings;

        private LightMode currentLightMode;

        public LightMode CurrentLightMode
        {
            get { return currentLightMode; }
            set
            {
                if(currentLightMode != value)
                {
                    StopAllCoroutines();
                    currentLightMode = value;

                    switch(currentLightMode)
                    {
                        case LightMode.Bounce:
                            StartCoroutine(Bounce(bounceSettings.maxIntensity, bounceSettings.minIntensity));
                            break;

                        case LightMode.Flash:
                            StartCoroutine(Flash());
                            break;

                        case LightMode.Flicker:
                            StartCoroutine(Flicker());
                            break;

                        case LightMode.Off:
                            DeactivateLight();
                            break;

                        case LightMode.On:
                            ActivateLight();
                            break;
                    }
                }
            }
        }


        private void ActivateLight()
        {
            controlledLight.intensity = lightSettings.baseIntensity;
        }


        private void DeactivateLight()
        {
            controlledLight.intensity = 0;
        }


        private IEnumerator Flicker()
        {
            yield return new WaitForSeconds(Random.Range(flickerSettings.minDelay, flickerSettings.maxDelay));
            controlledLight.intensity = Random.Range(flickerSettings.minIntensity, flickerSettings.maxIntensity);
            StartCoroutine(Flicker());
        }


        private IEnumerator Bounce(float from, float to)
        {
            float elapsedTime = 0f;

            while(elapsedTime < bounceSettings.bounceSpeed)
            {
                controlledLight.intensity = Mathf.Lerp(from, to, elapsedTime / bounceSettings.bounceSpeed);

                elapsedTime += Time.fixedDeltaTime;
                yield return null;
            }

            StartCoroutine(Bounce(to, from));
        }


        private IEnumerator Flash()
        {
            controlledLight.intensity = flashSettings.maxIntensity;
            yield return new WaitForSeconds(flashSettings.onTime);

            controlledLight.intensity = flashSettings.minIntensity;
            yield return new WaitForSeconds(flashSettings.offTime);

            StartCoroutine(Flash());
        }


        private void FixedUpdate()
        {
            CurrentLightMode = lightMode;
        }
    }
}

