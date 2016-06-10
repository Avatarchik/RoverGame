using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class HoverTip : Menu
    {
        public RectTransform background;
        public Text itemName;
        public Text itemDescription;


        public void Open(string name, string description, Vector3 location, bool inventory = true)
        {
            Vector3 openPosition = location;
            itemName.text = name;
            itemDescription.text = description;


            if(inventory)
            {
                openPosition = new Vector3(Input.mousePosition.x - 512f + background.rect.width / 2, Input.mousePosition.y - 384f + background.rect.height / 2, 0f);//new Vector3(location.x + background.rect.width / 2, location.y + background.rect.height / 2, location.z);
            }
            else
            {
                openPosition = new Vector3(Input.mousePosition.x - 512f + background.rect.width / 2, Input.mousePosition.y - 384f + background.rect.height / 2, 0f);//new Vector3(location.x - background.rect.width / 2, location.y + background.rect.height / 2, location.z);
            }

            GetComponent<RectTransform>().position = openPosition;

            base.Open();
        }
    }
}
