using UnityEngine;
using System.Collections;

public class CharacterDriver : MonoBehaviour
{
    public PlayerStats playerStats;

    public CameraDriver cameraDriver;

    public AudioSource servoMotorSound;
    

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

    }
}
