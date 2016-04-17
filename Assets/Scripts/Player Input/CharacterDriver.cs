using UnityEngine;
using System.Collections;

namespace Sol
{
    public class CharacterDriver : MonoBehaviour
    {
        public PlayerStats playerStats;

        public CameraDriver cameraDriver;

        public Light flashLight;

        public bool strafing = true;
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
            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                //CachedSoundManager.Play(ROVER_MOVEMENT_SOUND_ID);
            }

            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                //CachedSoundManager.Stop(ROVER_MOVEMENT_SOUND_ID);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                flashLightActive = !flashLightActive;
                flashLight.gameObject.SetActive(flashLightActive);
            }

            if (Input.GetKeyDown(KeyCode.B)) strafing = !strafing;
            if (Input.GetKeyDown(KeyCode.R))Reset();
        }


        private void FixedUpdate()
        {
            if (Input.GetAxis("Vertical") != 0)
            {
                transform.Translate(Vector3.forward * playerStats.CurrentMovementSpeed * Time.fixedDeltaTime * Input.GetAxis("Vertical") * movementSpeedMultiplier);
            }
            if(strafing)
            {
                if (Input.GetAxis("Horizontal") != 0)
                {
                    transform.Translate(Vector3.right * playerStats.CurrentMovementSpeed * Time.fixedDeltaTime * Input.GetAxis("Horizontal") * movementSpeedMultiplier);
                }
            }
            else
            {
                if (Input.GetAxis("Horizontal") != 0)
                {
                    transform.Rotate(Vector3.up, playerStats.TurnSpeed *Time.fixedDeltaTime * Input.GetAxis("Horizontal") * movementSpeedMultiplier * 18f);
                }
            }
            
        }


        private void Awake()
        {
            gameObject.GetComponent<Animator>().enabled = false;
        }
    }

}
