using UnityEngine;
using System.Collections;

namespace Sol
{
    public class CharacterDriver : MonoBehaviour
    {
        private const int ROVER_MOVEMENT_SOUND_ID = 10;

        public PlayerStats playerStats;

        public CameraDriver cameraDriver;

        public Light flashLight;

        public float movementSpeedMultiplier = 1.75f;

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


        private void Reset()
        {
            transform.rotation = Quaternion.identity;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
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

            if (Input.GetKeyDown(KeyCode.R))
            {
                Reset();
            }
        }


        private void FixedUpdate()
        {
            if (Input.GetAxis("RoverMove") != 0)
            {
                transform.Translate(Vector3.forward * playerStats.MoveSpeed * Time.fixedDeltaTime * Input.GetAxis("RoverMove") * movementSpeedMultiplier);
            }

            if (Input.GetAxis("RoverTurn") != 0)
            {
                transform.Translate(Vector3.right * playerStats.MoveSpeed * Time.fixedDeltaTime * Input.GetAxis("RoverTurn") * movementSpeedMultiplier);
            }
        }


        private void Awake()
        {
            gameObject.GetComponent<Animator>().enabled = false;
        }
    }

}
