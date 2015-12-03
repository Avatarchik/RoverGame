using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    public GameObject root;

    private bool isActive = false;

    public bool IsActive
    {
        get { return isActive; }
    }

    public virtual void Open()
    {
        isActive = true;
        root.SetActive(true);
    }


    public virtual void Close()
    {
        isActive = false;
        root.SetActive(false);
    }
}
