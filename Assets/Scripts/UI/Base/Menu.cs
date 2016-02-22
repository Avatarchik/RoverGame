using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    public delegate void MenuOpen();
    public static event MenuOpen OnMenuOpen;

    public delegate void MenuClose();
    public static event MenuClose OnMenuClose;

    public GameObject root;
    public bool stopsMovement = true;

    protected bool isActive = false;

    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    public virtual void Open()
    {
        if(!IsActive)
        {
            isActive = true;
            root.SetActive(true);
            if (stopsMovement && OnMenuClose != null) OnMenuOpen();
        }
    }


    public virtual void Close()
    {
        if(IsActive)
        {
            isActive = false;
            root.SetActive(false);
            if (stopsMovement) OnMenuClose();
        }
    }
}
