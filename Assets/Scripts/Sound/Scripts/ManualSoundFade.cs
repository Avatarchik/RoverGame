using UnityEngine;
using System.Collections;

public class ManualSoundFade : MonoBehaviour
{
    public AudioSource controlledAudio;
    public float minVolume = 0f;
    public float maxVolume = 1f;
    public float fadeTime = 2f;


    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while(elapsedTime < fadeTime)
        {
            controlledAudio.volume = Mathf.Lerp(minVolume, maxVolume, elapsedTime / fadeTime);

            elapsedTime += Time.fixedDeltaTime;
            yield return null;
        }
    }


    private void Awake()
    {
        StartCoroutine(FadeIn());
    }
}
