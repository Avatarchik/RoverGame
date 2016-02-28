using UnityEngine;
using System.Collections;

public class LightMessage : MonoBehaviour
{
    public GameObject flashLight;


    private void Update()
    {
        if(transform.localEulerAngles.x > 180 || transform.localEulerAngles.x < 0 && !UIManager.GetMenu<MessageMenu>().IsActive)
        {
            if(!flashLight.activeSelf)
            {
                UIManager.GetMenu<MessageMenu>().Open("'F' to open flashlight");
            }
            else
            {
                UIManager.GetMenu<MessageMenu>().Close();
            }
            
        }
    }
}
