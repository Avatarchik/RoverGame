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

    public float minZoom = 10;
    public float maxZoom = 70;

    public float zoomSpeed = 20.0f;

    public AudioSource servoMotorSound;

    private float rotationY = 0.0f;
    private float rotationX = 0.0f;

    private float rotationYL = 0.0f;
    private float rotationXL = 0.0f;

    private float zoom;

    private PlayerStats playerStats;

    private void Update ()
    {
        if(playerStats.TurnSpeed > 0)
        {
            rotationX += Input.GetAxis("Mouse X") * sensX * Time.deltaTime * playerStats.TurnSpeed;
            rotationY += Input.GetAxis("Mouse Y") * sensY * Time.deltaTime * playerStats.TurnSpeed;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);

            if (rotationY != rotationYL || rotationX != rotationXL)
            {
                //will need to play audio too
                playerCamera.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
                //playerCamera.transform.localEulerAngles = new Vector3(0, rotationX, 0);
                if (!servoMotorSound.isPlaying) servoMotorSound.Play();
            }

            else
            {
                if (servoMotorSound.isPlaying) servoMotorSound.Pause();
            }

            rotationXL = rotationX;
            rotationYL = rotationY;

            playerCamera.fieldOfView = Mathf.Clamp(playerCamera.fieldOfView + (Input.GetAxis("Mouse ScrollWheel") * -zoomSpeed), minZoom, maxZoom);
        }
    }


    private void Awake()
    {
        playerStats = GameObject.FindObjectOfType<PlayerStats>() as PlayerStats;
    }
}
