using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RangeFinder : MonoBehaviour
{
    public Scrollbar scrollbar;

    private bool scrolling = false;
    private float target = Random.Range(0f, 1f);
    private float current = 0f;
    private float elapsedTime = 0f;
    private float desiredTime = Random.Range(1f, 5f);

    private void Update()
    {
        if(!scrolling)
        {
            scrolling = true;

            current = scrollbar.value;
            target = Random.Range(0f, 1f);
            desiredTime = Random.Range(5f, 10f);
            elapsedTime = 0f;
        }
        else
        {
            scrollbar.value = Mathf.Lerp(current, target, elapsedTime / desiredTime);
            elapsedTime += Time.deltaTime;

            if (elapsedTime > desiredTime) scrolling = false;
        }
    }
}
