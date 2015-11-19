using UnityEngine;
using System.Collections;

public class CharacterDriver : MonoBehaviour
{
    public PlayerStats playerStats;

    public AudioSource servoMotorSound1;
    public AudioSource servoMotorSound2;

    // Update is called once per frame
    private void Update ()
    {
        if (Input.GetAxis("RoverMove") != 0)
        {
            if(!servoMotorSound1.isPlaying) servoMotorSound1.Play();
            transform.Translate(Vector3.forward * playerStats.stats[playerStats.MOVE_SPEED_ID].StatValue * 0.01f * Input.GetAxis("RoverMove"));
        } else {
            if (servoMotorSound1.isPlaying) servoMotorSound1.Pause();
        }

        if (Input.GetAxis("RoverTurn") != 0)
        {
            if (!servoMotorSound2.isPlaying) servoMotorSound2.Play();
            gameObject.transform.Rotate(Vector3.up * playerStats.stats[playerStats.TURN_SPEED_ID].StatValue * Input.GetAxis("RoverTurn"));
        } else {
            if (servoMotorSound2.isPlaying) servoMotorSound2.Pause();
            
        }
    }
}
