using UnityEngine;
using System.Collections;

namespace Sol
{
    public class DeadBatteryMenu : Menu
    {
        public float timeLapseModifier = 10f;

        private AutoIntensity cachedAutoIntensity;

        public AutoIntensity CachedAutoIntensity
        {
            //TODO dont relly on gameobject.find by name here
            get { return (cachedAutoIntensity != null) ? cachedAutoIntensity : cachedAutoIntensity = GameObject.Find("Sun").GetComponent<AutoIntensity>(); }
        }


        public override void Open()
        {
            if(!IsActive)
            {
                base.Open();

                Vector3 dayRotation = CachedAutoIntensity.dayRotateSpeed;
                Vector3 nightRotation = CachedAutoIntensity.nightRotateSpeed;

                CachedAutoIntensity.dayRotateSpeed = new Vector3(dayRotation.x * timeLapseModifier, dayRotation.y, dayRotation.z);
                CachedAutoIntensity.nightRotateSpeed = new Vector3(nightRotation.x * timeLapseModifier, nightRotation.y, nightRotation.z);
            }
        }


        public override void Close()
        {
            if(IsActive)
            {
                base.Close();
                IsActive = false;
                Vector3 dayRotation = CachedAutoIntensity.dayRotateSpeed;
                Vector3 nightRotation = CachedAutoIntensity.nightRotateSpeed;

                CachedAutoIntensity.dayRotateSpeed = new Vector3(dayRotation.x / timeLapseModifier, dayRotation.y, dayRotation.z);
                CachedAutoIntensity.nightRotateSpeed = new Vector3(nightRotation.x / timeLapseModifier, nightRotation.y, nightRotation.z);
            }
        }
    }
}

