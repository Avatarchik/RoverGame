using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class Hud : MonoBehaviour
    {
        public GameObject root;
        public PlayerStats player;
        public float showComponentFade;
        public float showComponentWait;

        public Slider healthBar;

        public List<RoverComponent> roverComponents = new List<RoverComponent>();


        public void Open()
        {
            root.SetActive(true);
        }


        public void Close()
        {
            root.SetActive(false);
        }
    }

}
