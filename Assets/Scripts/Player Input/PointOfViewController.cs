using UnityEngine;
using System.Collections;

public class PointOfViewController : MonoBehaviour
{
    public Transform viewCamera;

    public Transform firstPersonPosition;
    public Transform thirdPersonPosition;

    public CameraCollision cameracollision;

    private bool isFirstPerson = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            isFirstPerson = !isFirstPerson;
            if (isFirstPerson)
            {
                cameracollision.enabled = false;
                viewCamera.localPosition = firstPersonPosition.localPosition;
                
            }
            else
            {
                cameracollision.enabled = true;
            }
        }
    }
}
