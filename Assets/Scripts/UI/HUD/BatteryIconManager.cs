using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BatteryIconManager : MonoBehaviour
{
    public Image batteryFill;
    public TimeOfDay timeOfDay;
    public PlayerStats playerStats;
    private float fillAmount = 0f;

    public float FillAmount
    {
        get { return fillAmount; }
        set
        {
            fillAmount = value;
        }
    }



    public void FixedUpdate()
    {
        if(timeOfDay.timeInSeconds > 0 && timeOfDay.timeInSeconds < timeOfDay.dayLength * 0.5f)
        {
            //its light outside, charge the battery!
            if(fillAmount < 1)
            {
                fillAmount += (Time.deltaTime * playerStats.stats[playerStats.RECHARGE_RATE_ID].StatValue) * 0.001f;
            }
        }
        else
        {
            //its dark outside, dont charge!
            if (fillAmount > 0)
            {
                fillAmount -= Time.deltaTime * 0.001f;
            }
            else
            {
                Debug.Log("player is dead");
            }
        }

        batteryFill.fillAmount = fillAmount;
    }
}
