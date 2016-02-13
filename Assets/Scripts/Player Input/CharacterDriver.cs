using UnityEngine;
using System.Collections;

public class CharacterDriver : MonoBehaviour
{
    public PlayerStats playerStats;

    public CameraDriver cameraDriver;

    public Light flashLight;

    public AudioSource servoMotorSound;

    private bool flashLightActive = false;
    

    private void FixedUpdate ()
    {
        if (Input.GetAxis("RoverMove") != 0)
        {
            if(!servoMotorSound.isPlaying) servoMotorSound.Play();
            transform.Translate(Vector3.forward * playerStats.MoveSpeed * Time.fixedDeltaTime * Input.GetAxis("RoverMove"));
        }

        if (Input.GetAxis("RoverTurn") != 0)
        {
            if (!servoMotorSound.isPlaying) servoMotorSound.Play();
            transform.Rotate(Vector3.up, playerStats.MoveSpeed * Time.fixedDeltaTime * Input.GetAxis("RoverTurn") * 25f);
        }

        if (Input.GetAxis("RoverStrafe") != 0)
        {
            if (!servoMotorSound.isPlaying) servoMotorSound.Play();
            transform.Translate(Vector3.right * playerStats.MoveSpeed * Time.fixedDeltaTime * Input.GetAxis("RoverStrafe"));
        }

        if (Input.GetAxis("RoverTurn") == 0 && Input.GetAxis("RoverMove")  == 0 && Input.GetAxis("RoverStrafe") == 0)
        {
            if (servoMotorSound.isPlaying) servoMotorSound.Pause();
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            flashLightActive = !flashLightActive;
            flashLight.gameObject.SetActive(flashLightActive);
        }

    }


    private void Awake()
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }
}
