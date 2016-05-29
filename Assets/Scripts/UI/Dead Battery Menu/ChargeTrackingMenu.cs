using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class ChargeTrackingMenu : Menu
    {
        public Text chargeText;
        public Image chargeGraphic;

        public Color chargedColor = Color.green;
        public Color midColor = Color.yellow;
        public Color drainedColor = Color.red;

        private PlayerStats cachedPlayerStats;

        public PlayerStats CachedPlayerStats
        {
            get { return (cachedPlayerStats != null) ? cachedPlayerStats : cachedPlayerStats = GameManager.Get<PlayerStats>(); }
        }


        public override void Open()
        {
            if(!IsActive)
            {
                Debug.Log("opening");

                base.Open();

                if (CachedPlayerStats.OverallCharge > CachedPlayerStats.MaxCharge * 0.89f && CachedPlayerStats.OverallCharge < CachedPlayerStats.MaxCharge * 0.91f)
                {
                    chargeGraphic.color = chargedColor;
                }
                else if (CachedPlayerStats.OverallCharge > CachedPlayerStats.MaxCharge * 0.49f && CachedPlayerStats.OverallCharge < CachedPlayerStats.MaxCharge * 0.51f)
                {
                    chargeGraphic.color = midColor;
                }
                else if (CachedPlayerStats.OverallCharge > CachedPlayerStats.MaxCharge * 0.01f && CachedPlayerStats.OverallCharge < CachedPlayerStats.MaxCharge * 0.1f)
                {
                    chargeGraphic.color = drainedColor;
                    StartCoroutine(Pulse());
                }
            }
        }


        public override void Close()
        {
            if(IsActive)
            {
                Debug.Log("closing");
                base.Close();
                StopCoroutine(Pulse());
                IsActive = false;
            }
        }


        private void Update()
        {
            if (IsActive)
            {
                chargeText.text = Mathf.RoundToInt(CachedPlayerStats.OverallCharge).ToString();
            }
        }


        private IEnumerator Pulse(bool black = true)
        {
            float pulseTime = 0.5f;
            float elpasedTime = 0f;

            Color prevColor = chargeGraphic.color;
            Color targetColor = Color.black;
            if (!black) targetColor = drainedColor;

            while(elpasedTime < pulseTime)
            {
                chargeGraphic.color = Color.Lerp(prevColor, targetColor, elpasedTime / pulseTime);

                elpasedTime += Time.deltaTime;
                yield return null;
            }

            StartCoroutine(Pulse(!black));
        }
    }
}

