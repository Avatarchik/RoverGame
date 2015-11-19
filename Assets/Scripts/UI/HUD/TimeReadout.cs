using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeReadout : MonoBehaviour
{
    public Text dayString;
    public Text timeString;

    public TimeOfDay timeOfDay;

    private string defaultDayString = "000000";
    private string timeStringFormat = "{0}:{1}:{2}";
    private int cleaveCount = 1;
    private int dayCount = 0;


    private void Update()
    {
        if(timeOfDay.dayCount != dayCount)
        {
            dayCount = timeOfDay.dayCount;

            if(dayCount == 10)
            {
                cleaveCount = 2;
            }
            else if(dayCount == 100)
            {
                cleaveCount = 3;
            }
            else if (dayCount == 1000)
            {
                cleaveCount = 4;
            }
            else if (dayCount == 10000)
            {
                cleaveCount = 5;
            }
            else if (dayCount == 100000)
            {
                cleaveCount = 6;
            }

            dayString.text = defaultDayString.Substring(0, defaultDayString.Length - cleaveCount) + dayCount;
        }

        int total = Mathf.RoundToInt(timeOfDay.timeInSeconds);
        int hours = Mathf.FloorToInt(total / 3600);
        string hourString = "0";
        if(hours >= 10)
        {
            hourString = hours + "";
        }
        else
        {
            hourString = hourString + hours;
        }
        total -= hours * 3600;

        int minutes = Mathf.FloorToInt(total / 60);
        string minuteString = "0";
        if (minutes >= 10)
        {
            minuteString = minutes + "";
        }
        else
        {
            minuteString = minuteString + minutes;
        }
        total -= minutes * 60;
        string secondString = "0";
        if (total >= 10)
        {
            secondString = total + "";
        }
        else
        {
            secondString = secondString + total;
        }

        timeString.text = string.Format(timeStringFormat, hourString, minuteString, secondString);
    }
}
