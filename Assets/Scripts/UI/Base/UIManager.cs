using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Sol
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public class UIManager : MonoBehaviour
    {
        protected static UIManager instance;
        protected static Canvas canvasRoot;

        public List<Menu> prefabs = new List<Menu>();

        protected List<Menu> cachedMenus = new List<Menu>();


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

        ///<summary>
        ///Reset cached menus
        ///</summary>
        public static void ClearMenus()
        {
            if (!Exists) return;

            foreach (Menu menu in Instance.cachedMenus)
            {
                Destroy(menu);
            }

            Instance.cachedMenus.Clear();
        }

        /// <summary>
        /// Open menu of type T and close all others as  desired
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="closeOthers"></param>
        /// <returns></returns>
        public static T Open<T>(bool closeOthers = false) where T : Menu
        {
            if (!Exists) return null;

            if (closeOthers) CloseAll();

            T menu = GetMenu<T>();

            if (menu) menu.Open();

            return menu;
        }

        /// <summary>
        /// Close menu of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Close<T>() where T : Menu
        {
            if (!Exists) return null;

            T menu = GetMenu<T>();

            if (menu) menu.Close();

            return menu;
        }

        /// <summary>
        /// Close all menus (not recommended for use)
        /// </summary>
        public static void CloseAll()
        {
            foreach (Menu menu in Instance.cachedMenus)
            {
                if (menu) menu.Close();
            }
        }

        /// <summary>
        /// Retrieve menu without opening it, create it if it doesnt exist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetMenu<T>() where T : Menu
        {
            if (!Exists) return null;

            T menu = null;

            foreach (Menu m in Instance.cachedMenus)
            {
                if (m is T)
                {
                    menu = m as T;
                    break;
                }
            }

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

            if (menu && !Instance.cachedMenus.Contains(menu)) Instance.cachedMenus.Add(menu);
            
            return menu;
        }

        /// <summary>
        /// Create a new menu of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="menuPrefab"></param>
        /// <returns></returns>
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