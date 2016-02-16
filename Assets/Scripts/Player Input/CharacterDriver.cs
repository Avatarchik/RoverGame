using UnityEngine;
using System.Collections;

public class CharacterDriver : MonoBehaviour
{
    private const int ROVER_MOVEMENT_SOUND_ID = 10;

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


    private void Update()
    {
        if (Input.GetAxis("RoverMove") != 0 || Input.GetAxis("RoverTurn") != 0 || Input.GetAxis("RoverStrafe") != 0)
        {
            CachedSoundManager.Play(ROVER_MOVEMENT_SOUND_ID);
        }

        if (Input.GetAxis("RoverTurn") == 0 && Input.GetAxis("RoverMove") == 0 && Input.GetAxis("RoverStrafe") == 0)
        {
            CachedSoundManager.Stop(ROVER_MOVEMENT_SOUND_ID);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            flashLightActive = !flashLightActive;
            flashLight.gameObject.SetActive(flashLightActive);
        }
    }
    

    private void FixedUpdate ()
    {
        if (Input.GetAxis("RoverMove") != 0)
        {
            transform.Translate(Vector3.forward * playerStats.MoveSpeed * Time.fixedDeltaTime * Input.GetAxis("RoverMove") * 1.5f);
        }

        if (Input.GetAxis("RoverTurn") != 0)
        {
            transform.Rotate(Vector3.up, playerStats.MoveSpeed * Time.fixedDeltaTime * Input.GetAxis("RoverTurn") * 25f);
        }

        if (Input.GetAxis("RoverStrafe") != 0)
        {
            transform.Translate(Vector3.right * playerStats.MoveSpeed * Time.fixedDeltaTime * Input.GetAxis("RoverStrafe"));
        }
    }


    private void Awake()
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }
}
