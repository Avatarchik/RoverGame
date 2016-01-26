using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public List<Menu> prefabs = new List<Menu>();

    protected List<Menu> menus = new List<Menu>();

    
    public void ClearMenus()
    {
        foreach(Menu menu in menus)
        {
            Destroy(menu.gameObject);
        }

        menus.Clear();
    }


    public static UIManager Instance
    {
        get
        {
            return (instance != null) ? instance : instance = GameObject.FindObjectOfType<UIManager>();
        }
    }


    public static bool Exists
    {
        get { return instance != null; }
    }


    public static Canvas CanvasRoot
    {
        get { return Instance.GetComponent<Canvas>(); }
    }


    public static T Open<T>() where T : Menu
    {
        if (!Exists) return null;

        T menu = GetMenu<T>();

        if (menu) menu.Open();

        return menu;
    }


    public static T Close<T>() where T : Menu
    {
        if (!Exists) return null;

        T menu = GetMenu<T>();

        if (menu) menu.Close();

        return menu;
    }


    public static T GetMenu<T>() where T : Menu
    {
        if (!Exists) return null;

        T menu = Instance.menus.FirstOrDefault(m => m is T) as T;

        if (!menu) menu = FindObjectOfType<T>();

        if (!menu) menu = Create<T>(Instance.prefabs.FirstOrDefault(m => m is T) as T);

        if(menu)
        {
            if (!Instance.menus.Contains(menu)) Instance.menus.Add(menu);
        }

        return menu;
    }


    public static T Create<T>(T menuPrefab) where T : Menu
    {
        T menu = null;

        if(menuPrefab != null)
        {
            menu = GameObject.Instantiate(menuPrefab) as T;
            menu.transform.SetParent(Instance.transform, false);
        }

        return menu;
    }
}
