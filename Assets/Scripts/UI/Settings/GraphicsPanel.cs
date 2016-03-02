using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ScionEngine;

namespace Sol
{
    public class GraphicsPanel : MonoBehaviour
    {
        public GameObject root;
        public Dropdown fullScreenDropdown;
        public Dropdown vsyncDropdown;
        public Dropdown bloomDropdown;

        private ScionPostProcess scion;

        public ScionPostProcess Scion
        {
            get { return (scion != null) ? scion : scion = GameObject.FindObjectOfType<ScionPostProcess>(); }
        }


        public void Activate()
        {
            root.SetActive(true);
        }


        public void Deactivate()
        {
            root.SetActive(false);
        }
        

        private void SetFullScreen(int i)
        {
            switch(i)
            {
                case 0:
                    if(Screen.fullScreen != true) Screen.fullScreen = true;
                    break;

                case 1:
                    if (Screen.fullScreen != false) Screen.fullScreen = false;
                    break;
            }
        }


        private void SetVSync(int i)
        {
            switch (i)
            {
                case 0:
                    QualitySettings.vSyncCount = 2;
                    break;

                case 1:
                    QualitySettings.vSyncCount = 1;
                    break;

                case 2:
                    QualitySettings.vSyncCount = 0;
                    break;
            }
        }


        private void SetBloom(int i)
        {
            switch (i)
            {
                case 0:
                    Scion.bloom = true;
                    break;

                case 1:
                    Scion.bloom = false;
                    break;
            }
        }


        private void Awake()
        {
            fullScreenDropdown.onValueChanged.AddListener(SetFullScreen);
            vsyncDropdown.onValueChanged.AddListener(SetVSync);
            bloomDropdown.onValueChanged.AddListener(SetBloom);
        }
    }
}

