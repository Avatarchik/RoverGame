using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Sol
{
    public class InGameMainMenu : Menu
    {
        public Toggle openMapToggle;
        public Toggle openCraftingToggle;
        public Toggle openInventoryToggle;
        public Toggle openLogFilesToggle;
        public Toggle openSystemToggle;

        public Button externalLink;

        public string targetUrl = "";

        public override void Close()
        {
            base.Close();
        }


        public void OpenMap(bool b = true)
        {
            //TODO implement a map menu
            if (b) { }
        }


        public void OpenCrafting(bool b = true)
        {
            if (b)
            {
                if (!IsActive) Open();
                CloseAll();
                UIManager.Open<Crafting>();
            }
        }


        public void OpenInventory(bool b = true)
        {
            if (b)
            {
                if (!IsActive) Open();
                OpenInventoryTransfer(b, false);
            }
        }


        public void OpenInventoryTransfer(bool b = true, bool transfer = false)
        {
            if (b)
            {
                UIManager.Close<SystemMenu>();
                UIManager.Close<LogMenu>();
                if (!IsActive) Open();
                Inventory inventory= UIManager.GetMenu<Inventory>();
                
                inventory.Open(transfer);
                openInventoryToggle.Select();
                Open();
            }
        }


        public void OpenLogs(bool b = true)
        {
            if (b)
            {
                UIManager.Close<SystemMenu>();
                UIManager.Close<Inventory>();
                if (!IsActive) Open();

                UIManager.Open<LogMenu>();
                Open();
            }
        }


        public void OpenSystem(bool b = true)
        {
            if(b)
            {
                UIManager.Close<LogMenu>();
                UIManager.Close<Inventory>();
                if (!IsActive) Open();

                UIManager.GetMenu<SystemMenu>().OpenGraphics() ;
                Open();
            }
        }


        private void CloseAll()
        {
            UIManager.Close<Crafting>();
            UIManager.Close<Container>();

            UIManager.Close<SystemMenu>();
            UIManager.Close<LogMenu>();
            UIManager.Close<Inventory>();
        }


        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
            {
                if(IsActive || UIManager.GetMenu<Crafting>().IsActive)
                {
                    CloseAll();
                    Close();
                }
                else
                {
                    CloseAll();
                    OpenSystem();
                    openSystemToggle.Select();
                }
            }

            if(Input.GetKeyDown(KeyCode.I))
            {
                if (IsActive)
                {
                    CloseAll();
                    Close();
                }
                else
                {
                    CloseAll();
                    OpenInventoryTransfer(true, false);
                    openInventoryToggle.Select();
                }
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                if (IsActive)
                {
                    CloseAll();
                    Close();
                }
                else
                {
                    CloseAll();
                    UIManager.GetMenu<Inventory>().Close();
                    OpenLogs();
                    openLogFilesToggle.Select();
                }
            }
        }


        private void OpenExternalLink()
        {
            Debug.Log("Opening URL : "+targetUrl);
            Application.OpenURL(targetUrl);
        }


        private void Awake()
        {
            openMapToggle.onValueChanged.AddListener(OpenMap);
            openCraftingToggle.onValueChanged.AddListener(OpenCrafting);
            openInventoryToggle.onValueChanged.AddListener(OpenInventory);
            openLogFilesToggle.onValueChanged.AddListener(OpenLogs);
            openSystemToggle.onValueChanged.AddListener(OpenSystem);

            //externalLink.onClick.AddListener(OpenExternalLink);
        }


    }
}

