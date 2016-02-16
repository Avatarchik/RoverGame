using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDown : Menu
{
    public Text countDownText;

    public override void Open()
    {
        base.Open();
    }


    public void Open(int desiredTime)
    {
        Open();
        StopAllCoroutines();
        StartCoroutine(CountDownCoroutine(desiredTime));
    }


    public void SetText(int desiredTime)
    {
        if(!IsActive)
        {
            Open(desiredTime);
            return;
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(CountDownCoroutine(desiredTime));
        }
    }


    public override void Close()
    {
        base.Close();
    }


    private IEnumerator CountDownCoroutine(int desiredtime)
    {
        int elapsedSeconds = 0;

        while(elapsedSeconds <= desiredtime)
        {
            countDownText.text = (desiredtime - elapsedSeconds).ToString();

            elapsedSeconds++;
            yield return new WaitForSeconds(1);
        }

        yield return new WaitForSeconds(0.5f);
        Close();
    }
}
