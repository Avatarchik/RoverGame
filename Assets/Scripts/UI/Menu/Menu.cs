using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    public delegate void MenuOpen();
    public static event MenuOpen OnMenuOpen;

    public delegate void MenuClose();
    public static event MenuClose OnMenuClose;

    public GameObject root;

    [HideInInspector]
    public bool isActive = false;

    public bool IsActive
    {
        get { return isActive; }
    }

    public virtual void Open()
    {
        isActive = true;
        root.SetActive(true);
        OnMenuOpen();
    }


    public virtual void Close()
    {
        isActive = false;
        root.SetActive(false);
        OnMenuClose();
    }
}
