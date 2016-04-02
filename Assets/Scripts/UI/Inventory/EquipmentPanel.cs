using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class EquipmentPanel : MonoBehaviour
    {
        public Image cameraIcon;
        public Image panelsIcon;
        public Image wheelsIcon;
        public Image antennaIcon;
        public Image chassiIcon;
        public Image batteryIcon;

        private PlayerStats cachedPlayerStats;

        public PlayerStats CachedPlayerStats
        {
            get { return (cachedPlayerStats != null) ? cachedPlayerStats : cachedPlayerStats = GameObject.FindObjectOfType<PlayerStats>(); }
        }


        public void Initialize()
        {
            foreach(RoverComponent rc in CachedPlayerStats.roverComponents)
            {
                switch(rc.currentComponentType)
                {
                    case RoverComponent.ComponentType.Antenna:
                        break;

                    case RoverComponent.ComponentType.Battery:
                        break;

                    case RoverComponent.ComponentType.Camera:
                        break;

                    case RoverComponent.ComponentType.Chassi:
                        break;

                    case RoverComponent.ComponentType.Panels:
                        break;

                    case RoverComponent.ComponentType.Wheels:
                        break;
                }
            }
        }
    }

}
