using UnityEngine;
using System.Collections;

public class CameraDriver : MonoBehaviour
{
    public Camera playerCamera;
    public Transform model;

    public float minX = -360.0f;
    public float maxX = 360.0f;

    public float minY = -45.0f;
    public float maxY = 45.0f;

    public float sensX = 100.0f;
    public float sensY = 100.0f;

    public AudioSource servoMotorSound;

    private float rotationY = 0.0f;
    private float rotationX = 0.0f;

    private float zoomSpeed = 2.0f;

    private float rotationYL = 0.0f;
    private float rotationXL = 0.0f;


    void Update ()
    {
        rotationX += Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        if (rotationY != rotationYL || rotationX != rotationXL)
        {
            //will need to play audio too
            playerCamera.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
            model.localEulerAngles = new Vector3(0, rotationX, 0);
            if (!servoMotorSound.isPlaying) servoMotorSound.Play();
        }
        else
        {
            if (servoMotorSound.isPlaying) servoMotorSound.Pause();
        }

        rotationXL = rotationX;
        rotationYL = rotationY;
    }
}
