using UnityEngine;
using System.Collections;

namespace Sol
{
    public class IndoorTrigger : MonoBehaviour
    {
        public delegate void WeatherEvent(bool indoors);
        public static event WeatherEvent OnWeatherEvent;

        private const string PLAYER_TAG = "Player";


        protected void OnTriggerEnter(Collider other)
        {
            if (other.tag == PLAYER_TAG)
            {
                OnWeatherEvent(true);
            }
        }


        protected void OnTriggerExit(Collider other)
        {
            if(other.tag == PLAYER_TAG)
            {
                OnWeatherEvent(false);
            }
        }
    }
}