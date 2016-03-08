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


        public override void Close()
        {
            UIManager.Open<ObjectiveTracker>();
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
                CloseAll();
                OpenInventoryTransfer(b, false);
            }
        }


        public void OpenInventoryTransfer(bool b = true, bool transfer = false)
        {
            if (b)
            {
                if (!IsActive) Open();
                Inventory inventory= UIManager.GetMenu<Inventory>();
                
                inventory.Open(transfer);
                openInventoryToggle.Select();
            }
        }


        public void OpenLogs(bool b = true)
        {
            if (b)
            {
                if (!IsActive) Open();
                CloseAll();
                UIManager.Open<LogMenu>();
            }
        }


        public void OpenSystem(bool b = true)
        {
            if(b)
            {
                if (!IsActive) Open();
                CloseAll();
                UIManager.GetMenu<SystemMenu>().OpenGraphics() ;
            }
        }


        private void CloseAll()
        {
            UIManager.Close<Crafting>();
            UIManager.Close<Inventory>();
            UIManager.Close<LogMenu> ();
            UIManager.Close<Container>();
            UIManager.Close<SystemMenu>();
        }


        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
            {
                if(IsActive)
                {
                    CloseAll();
                    Close();
                }
                else
                {
                    Open();
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
                    Open();
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
                    Open();
                    OpenLogs();
                    openLogFilesToggle.Select();
                }
            }
        }


        private void Awake()
        {
            openMapToggle.onValueChanged.AddListener(OpenMap);
            openCraftingToggle.onValueChanged.AddListener(OpenCrafting);
            openInventoryToggle.onValueChanged.AddListener(OpenInventory);
            openLogFilesToggle.onValueChanged.AddListener(OpenLogs);
            openSystemToggle.onValueChanged.AddListener(OpenSystem);
        }


    }
}

