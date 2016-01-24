using UnityEngine;
using System.Collections;

public class TimeOfDay : MonoBehaviour
{
    public float minutesPerSecond = 5f;
    public float timeInSeconds = 0.0f;

    public int dayCount = 0;

    public float dayLength;


    private IEnumerator Tick()
    {
        float elapsedTime = 0f;
        while(elapsedTime < 1f)
        {
           // gameObject.transform.Rotate(Vector3.right, (minutesPerSecond / dayLength)*360);

            if (timeInSeconds < dayLength)
            {
                timeInSeconds += 1f * minutesPerSecond;
            }
            else
            {
                timeInSeconds = 0.0f;
                dayCount++;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        

        StartCoroutine(Tick ());
    }

    private void Awake()
    {
        //exact length of a martian day : 24:37:22
        dayLength = (24 * 60 * 60) + (37 * 60) + 22;
        StartCoroutine(Tick());
    }
}
