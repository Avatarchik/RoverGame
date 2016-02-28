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


        public void OpenMap(bool b)
        {
            //TODO implement a map menu
            if (b) { }
        }


        public void OpenCrafting(bool b)
        {
            if(b) UIManager.Open<Crafting>();
        }


        public void OpenInventory(bool b)
        {
            if(b) UIManager.Open<Inventory>();
        }


        public void OpenLogs(bool b)
        {
            if(b) UIManager.Open<LogMenu>();
        }


        public void OpenSystem(bool b)
        {
            if(b) { }
            //TODO implement systems menu
            //UIManager.Open<SystemM>();
        }


        private void CloseAll()
        {
            UIManager.Close<Crafting>();
            UIManager.Close<Inventory>();
            UIManager.Close<LogMenu> ();
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

