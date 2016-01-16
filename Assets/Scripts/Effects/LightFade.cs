using UnityEngine;
using System.Collections;

public class LightFade : MonoBehaviour
{
    public Light fadeLight;

    public Color lightColor;

    public float maxIntensity;
    public float minIntensity;
    public float fadeTime;


    private IEnumerator Bounce()
    {
        fadeLight.color = lightColor;
        float elapsedTime = 0f;
        while(elapsedTime < fadeTime)
        {
            fadeLight.intensity = Mathf.Lerp(maxIntensity, minIntensity, elapsedTime / fadeTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeLight.intensity = minIntensity;
        elapsedTime = 0;
        yield return new WaitForSeconds(0.3f);

        while (elapsedTime < fadeTime)
        {
            fadeLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, elapsedTime / fadeTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(Bounce());
    }


    private void Start ()
    {
        StartCoroutine(Bounce());
	}
}
