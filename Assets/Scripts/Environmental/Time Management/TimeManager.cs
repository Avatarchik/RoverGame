using UnityEngine;
using System.Collections;

namespace Sol
{
    public class TimeManager : MonoBehaviour
    {
        public enum TimeSetting
        {
            Dawn,
            Day,
            Dusk,
            Night
        }

        public AutoIntensity sunManager;

        public Vector3 dayRotateSpeed;
        public Vector3 nightRotateSpeed;

        public float fastForwardFactor = 5;

        //need way to fast forward by number of hours?
        //while key is pressed?
        public IEnumerator FastForwardTime()
        {
            sunManager.dayRotateSpeed *= fastForwardFactor;
            sunManager.nightRotateSpeed *= fastForwardFactor;

            while (Input.GetKey(KeyCode.P))
            {
                yield return null;
            }

            sunManager.dayRotateSpeed = dayRotateSpeed;
            sunManager.nightRotateSpeed = nightRotateSpeed;
        }


        //need way to freeze passage of time
        public void FreezeTime()
        {
            sunManager.dayRotateSpeed = Vector3.zero;
            sunManager.nightRotateSpeed = Vector3.zero;
        }


        //need way to start passage of time
        public void StartTime()
        {
            sunManager.dayRotateSpeed = dayRotateSpeed;
            sunManager.nightRotateSpeed = nightRotateSpeed;
        }


        public void SetTime(TimeSetting timeSetting)
        {
            //need to know what the rotational direction is?
        }


        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                StartCoroutine(FastForwardTime());
            }
        }

    }

}
