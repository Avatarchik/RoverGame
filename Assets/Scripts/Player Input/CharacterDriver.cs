using UnityEngine;
using System.Collections;

public class CharacterDriver : MonoBehaviour
{
    private const int ROVER_MOVEMENT_SOUND_ID = 2007596832;

    public PlayerStats playerStats;

    public CameraDriver cameraDriver;

    public Light flashLight;

    //public AudioSource servoMotorSound;

    private bool flashLightActive = false;
    private SoundManager cachedSoundManager;

    public SoundManager CachedSoundManager
    {
        get
        {
            if (cachedSoundManager == null) cachedSoundManager = GameManager.Get<SoundManager>();
            if (cachedSoundManager == null) cachedSoundManager = GameObject.FindObjectOfType<SoundManager>();

            return cachedSoundManager;
        }
    }
    

    private void FixedUpdate ()
    {
        if (Input.GetAxis("RoverMove") != 0)
        {
            // if(!servoMotorSound.isPlaying) servoMotorSound.Play();
            if (!CachedSoundManager.IsSoundclipPlaying(CachedSoundManager.Find(ROVER_MOVEMENT_SOUND_ID)))
                CachedSoundManager.Play(ROVER_MOVEMENT_SOUND_ID);
            transform.Translate(Vector3.forward * playerStats.MoveSpeed * Time.fixedDeltaTime * Input.GetAxis("RoverMove"));
        }

        if (Input.GetAxis("RoverTurn") != 0)
        {
            //     if (!servoMotorSound.isPlaying) servoMotorSound.Play();
            if (!CachedSoundManager.IsSoundclipPlaying(CachedSoundManager.Find(ROVER_MOVEMENT_SOUND_ID)))
                CachedSoundManager.Play(ROVER_MOVEMENT_SOUND_ID);
            transform.Rotate(Vector3.up, playerStats.MoveSpeed * Time.fixedDeltaTime * Input.GetAxis("RoverTurn") * 25f);
        }

        if (Input.GetAxis("RoverStrafe") != 0)
        {
            //  if (!servoMotorSound.isPlaying) servoMotorSound.Play();
            if(!CachedSoundManager.IsSoundclipPlaying(CachedSoundManager.Find(ROVER_MOVEMENT_SOUND_ID)))
                CachedSoundManager.Play(ROVER_MOVEMENT_SOUND_ID);
            transform.Translate(Vector3.right * playerStats.MoveSpeed * Time.fixedDeltaTime * Input.GetAxis("RoverStrafe"));
        }

        if (Input.GetAxis("RoverTurn") == 0 && Input.GetAxis("RoverMove")  == 0 && Input.GetAxis("RoverStrafe") == 0)
        {
            //   if (servoMotorSound.isPlaying) servoMotorSound.Pause();
            if (CachedSoundManager.IsSoundclipPlaying(CachedSoundManager.Find(ROVER_MOVEMENT_SOUND_ID)))
                CachedSoundManager.Stop(CachedSoundManager.Find(ROVER_MOVEMENT_SOUND_ID));

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
