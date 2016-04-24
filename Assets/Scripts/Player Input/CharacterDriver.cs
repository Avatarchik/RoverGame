using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

        public List<WheelCollider> frontWheels = new List<WheelCollider>();
        public List<WheelCollider> backWheels = new List<WheelCollider>();

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
            if (Input.GetAxis("Vertical") != 0)
            {
                //transform.Translate(Vector3.forward * playerStats.CurrentMovementSpeed * Time.fixedDeltaTime * Input.GetAxis("Vertical") * movementSpeedMultiplier);
                List<WheelCollider> wcs = new List<WheelCollider>();
                wcs.AddRange(frontWheels);
                wcs.AddRange(backWheels);
                foreach (WheelCollider wc in wcs)
                {
                    wc.brakeTorque = 0;
                    wc.motorTorque = playerStats.CurrentMovementSpeed * Time.fixedDeltaTime * Input.GetAxis("Vertical") * movementSpeedMultiplier;
                }
            }
            else
            {
                List<WheelCollider> wcs = new List<WheelCollider>();
                wcs.AddRange(frontWheels);
                wcs.AddRange(backWheels);
                foreach (WheelCollider wc in wcs)
                {
                    wc.brakeTorque = 15;
                    wc.motorTorque = wc.motorTorque *0.1f;
                }
            }

            if (Input.GetAxis("Horizontal") != 0)
            {
                foreach (WheelCollider wc in frontWheels)
                {
                    wc.steerAngle = Input.GetAxis("Horizontal") * 45f;
                }
            }
            else
            {
                foreach (WheelCollider wc in frontWheels)
                {
                    wc.steerAngle = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                flashLightActive = !flashLightActive;
                flashLight.gameObject.SetActive(flashLightActive);
            }

            if (Input.GetKeyDown(KeyCode.R))Reset();
        }


        private void FixedUpdate()
        {
            
            
        }


        private void Awake()
        {
            gameObject.GetComponent<Animator>().enabled = false;
        }
    }

}
