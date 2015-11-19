using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CameraEquip : MonoBehaviour
{
    public Image brokenCameraFilter;
    public RoverComponent cameraComponent;
    public RoverComponent wheelComponent;

    public List<Item> equippableItems = new List<Item>();

    public static RoverComponent CameraComponent;
    public static RoverComponent WheelComponent;
    public static List<Item> EquippableItems = new List<Item>();

    public static Image BrokenCameraFilter;

    public static void Equip(int componentId)
    {
        switch(componentId)
        {
            case 10:
                foreach(Item item in EquippableItems)
                {
                    if(item.id == componentId)
                        WheelComponent.equippedItem = item;
                }
                WheelComponent.Health = 100;
                
                break;

            case 20:
                foreach (Item item in EquippableItems)
                {
                    if (item.id == componentId)
                        CameraComponent.equippedItem = item;
                }
                BrokenCameraFilter.color = Color.clear;
                CameraComponent.Health = 100;
                break;
        }
        
    }

    
    private void Awake()
    {
        BrokenCameraFilter = brokenCameraFilter;
        CameraComponent = cameraComponent;
        WheelComponent = wheelComponent;

        EquippableItems = equippableItems;
    }
}
