using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BatteryIconManager : MonoBehaviour
{
    public Image batteryFill;

    public float fillAmount = 0f;

    public void FixedUpdate()
    {
        if(fillAmount >= 1)
        {
            fillAmount = 0;
        }
        else
        {
            fillAmount += Time.deltaTime;
        }

        batteryFill.fillAmount = fillAmount;
    }
}
