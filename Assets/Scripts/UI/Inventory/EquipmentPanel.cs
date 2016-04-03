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

        public Image cameraHud;
        public Image panelsHud;
        public Image wheelsHud;
        public Image antennaHud;
        public Image chassiHud;
        public Image batteryHud;

        private PlayerStats cachedPlayerStats;

        public PlayerStats CachedPlayerStats
        {
            get { return (cachedPlayerStats != null) ? cachedPlayerStats : cachedPlayerStats = GameObject.FindObjectOfType<PlayerStats>(); }
        }


        public void EquipItem(EquipableItem equippableItem)
        {
            foreach(RoverComponent rc in CachedPlayerStats.roverComponents)
            {
                if(rc.currentComponentType == equippableItem.componentType)
                {
                    //equipment match, swap it out!!
                    rc.equippedItem = equippableItem;

                    return;
                }
            }

            //we dont already have this item. add it i guess?
            RoverComponent roverComponent = new RoverComponent();
            roverComponent.currentComponentType = equippableItem.componentType;
            roverComponent.equippedItem = equippableItem;

            Initialize();
        }


        public void Initialize()
        {
            Debug.Log(CachedPlayerStats.roverComponents.Count);

            foreach(RoverComponent rc in CachedPlayerStats.roverComponents)
            {
                switch(rc.currentComponentType)
                {
                    case RoverComponent.ComponentType.Antenna:
                        Debug.Log("antenna");
                        antennaIcon.sprite = rc.equippedItem.image;
                        antennaIcon.color = rc.equippedItem.equipmentColor;

                        antennaHud.color = rc.equippedItem.equipmentColor;
                        break;

                    case RoverComponent.ComponentType.Battery:
                        Debug.Log("battery");
                        batteryIcon.sprite = rc.equippedItem.image;
                        batteryIcon.color = rc.equippedItem.equipmentColor;

                        batteryHud.color = rc.equippedItem.equipmentColor;
                        break;

                    case RoverComponent.ComponentType.Camera:
                        Debug.Log("camera");
                        cameraIcon.sprite = rc.equippedItem.image;
                        cameraIcon.color = rc.equippedItem.equipmentColor;

                        cameraHud.color = rc.equippedItem.equipmentColor;
                        break;

                    case RoverComponent.ComponentType.Chassi:
                        Debug.Log("chassi");
                        chassiIcon.sprite = rc.equippedItem.image;
                        chassiIcon.color = rc.equippedItem.equipmentColor;

                        chassiHud.color = rc.equippedItem.equipmentColor;
                        break;

                    case RoverComponent.ComponentType.Panels:
                        Debug.Log("panels");
                        panelsIcon.sprite = rc.equippedItem.image;
                        panelsIcon.color = rc.equippedItem.equipmentColor;

                        panelsHud.color = rc.equippedItem.equipmentColor;
                        break;

                    case RoverComponent.ComponentType.Wheels:
                        Debug.Log("wheels");
                        wheelsIcon.sprite = rc.equippedItem.image;
                        wheelsIcon.color = rc.equippedItem.equipmentColor;

                        wheelsHud.color = rc.equippedItem.equipmentColor;
                        break;
                }
            }
        }
    }

}
