using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    [RequireComponent(typeof(Canvas))]
    public class UIManager : MonoBehaviour
    {
        protected static UIManager instance;
        protected static Canvas canvasRoot;

        public List<Menu> prefabs = new List<Menu>();

        protected Dictionary<System.Type, Menu> cachedMenus = new Dictionary<System.Type, Menu>();


        public static UIManager Instance
        {
            get { return (instance != null) ? instance : instance = GameObject.FindObjectOfType<UIManager>(); }
        }


        public static bool Exists
        {
            get { return Instance != null; }
        }


        public static Canvas CanvasRoot
        {
            get { return (canvasRoot != null) ? canvasRoot : canvasRoot = Instance.GetComponent<Canvas>(); }
        }


        ///Reset cached menus
        public static void ClearMenus()
        {
            if (!Exists) return;

            foreach (System.Type type in Instance.cachedMenus.Keys)
            {
                Destroy(Instance.cachedMenus[type]);
            }

            Instance.cachedMenus.Clear();
        }


        ///Open menu of type T and close all others as  desired
        public static T Open<T>(bool closeOthers = false) where T : Menu
        {
            if (!Exists) return null;

            if (closeOthers) CloseAll();

            T menu = GetMenu<T>();

            if (menu) menu.Open();

            return menu;
        }


        ///Close menu of type T
        public static T Close<T>() where T : Menu
        {
            if (!Exists) return null;

            T menu = GetMenu<T>();

            if (menu) menu.Close();

            return menu;
        }

        //Close all menus
        public static void CloseAll()
        {
            foreach (System.Type type in Instance.cachedMenus.Keys)
            {
                if (Instance.cachedMenus[type]) Instance.cachedMenus[type].Close();
            }
        }


        ///Retrieve menu without opening it, create it if it doesnt exist.
        public static T GetMenu<T>() where T : Menu
        {
            if (!Exists) return null;

            T menu = null;

            System.Type type = typeof(T);
            if (Instance.cachedMenus.ContainsKey(type)) menu = Instance.cachedMenus[type] as T;

            if (!menu) menu = FindObjectOfType<T>();

            if(!menu)
            {
                foreach (Menu p in Instance.prefabs)
                {
                    if (p is T)
                    {
                        menu = Create<T>(p as T);
                        break;
                    }
                }
            }

            if (menu && !Instance.cachedMenus.ContainsKey(type)) Instance.cachedMenus.Add(type, menu);
            
            return menu;
        }


        ///Create a new menu of type T
        protected static T Create<T>(T menuPrefab) where T : Menu
        {
            T menu = null;

            if (menuPrefab != null)
            {
                menu = GameObject.Instantiate(menuPrefab) as T;
                menu.transform.SetParent(Instance.transform, false);
            }

            return menu;
        }
    }
}