using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public Animator doorAnimator;


    private bool isOpen = false;

    public bool IsOpen
    {
        get { return isOpen; }
        set
        {
            if(isOpen != value)
            {
                isOpen = value;

                if (isOpen)
                {
                    OpenDoor();
                }
                else
                {
                    CloseDoor();
                }
            }
        }
    }


    public virtual void OpenDoor()
    {
        
    }


    public virtual void CloseDoor()
    {

    }
}
