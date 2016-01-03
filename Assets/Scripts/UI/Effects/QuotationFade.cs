using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuotationFade : MonoBehaviour
{
    public float fadeInTime = 1.25f;
    public float waitTime = 2f;
    public float fadeOutTime = 0.75f;

    public Color fontColor = Color.white;

    public List<Text> introFadeText = new List<Text>();


    public IEnumerator Intro()
    {
        foreach (Text t in introFadeText)
        {
            StartCoroutine(Fader(fadeInTime, t, Color.clear, fontColor));
            yield return null;
        }

        yield return new WaitForSeconds(waitTime);

        foreach (Text t in introFadeText)
        {
            StartCoroutine(Fader(fadeOutTime, t, fontColor, Color.clear));
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        //Application.LoadLevel(1);
        SceneManager.LoadScene(1);
    }


    public IEnumerator Fader(float desiredTime, Text t, Color fromColor, Color toColor)
    {
        float elapsedTime = 0f;
        while (elapsedTime <= desiredTime)
        {
            t.color = Color.Lerp(fromColor, toColor, elapsedTime / desiredTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        t.color = toColor;
    }


    private void Awake()
    {
        StartCoroutine(Intro());
    }
}
