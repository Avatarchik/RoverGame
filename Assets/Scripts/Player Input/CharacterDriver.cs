using UnityEngine;
using System.Collections;

public class CharacterDriver : MonoBehaviour
{
    public PlayerStats playerStats;

    public CameraDriver cameraDriver;

    public Light flashLight;

    public AudioSource servoMotorSound;

    private bool flashLightActive = false;
    

    // Update is called once per frame
    private void Update ()
    {
        if (Input.GetAxis("RoverMove") != 0)
        {
            if(!servoMotorSound.isPlaying) servoMotorSound.Play();
            transform.Translate(cameraDriver.model.right * playerStats.MoveSpeed * -0.01f * Input.GetAxis("RoverMove"));
        }

        if (Input.GetAxis("RoverTurn") != 0)
        {
            if (!servoMotorSound.isPlaying) servoMotorSound.Play();
            transform.Translate(cameraDriver.model.forward * playerStats.MoveSpeed * 0.01f * Input.GetAxis("RoverTurn"));
        }

        if(Input.GetAxis("RoverTurn") == 0 && Input.GetAxis("RoverMove")  == 0)
        {
            if (servoMotorSound.isPlaying) servoMotorSound.Pause();
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            flashLightActive = !flashLightActive;
            flashLight.gameObject.SetActive(flashLightActive);
        }

    }
}
