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

        public List<WheelCollider> frontWheels = new List<WheelCollider>();
        public List<WheelCollider> backWheels = new List<WheelCollider>();

        private bool flashLightActive = false;
        private SoundManager cachedSoundManager;


        /// <summary>
        /// Reset player position to upright rotation one meter up
        /// </summary>
        private void Reset()
        {
            transform.rotation = Quaternion.identity;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                flashLightActive = !flashLightActive;
                flashLight.gameObject.SetActive(flashLightActive);
            }

            if (Input.GetKeyDown(KeyCode.R))Reset();
        }


        private void FixedUpdate()
        {
            //handle forward/backward movement
            if (Input.GetAxis("Vertical") != 0)
            {
                foreach (WheelCollider wc in frontWheels)
                {
                    wc.brakeTorque = 0;
                    wc.motorTorque = playerStats.CurrentMovementSpeed * Time.fixedDeltaTime * Input.GetAxis("Vertical") * movementSpeedMultiplier;
                }
            }
            else
            {
                if (frontWheels[0].rpm > 0)
                {
                    foreach (WheelCollider wc in frontWheels)
                    {
                        wc.brakeTorque = 15;
                        wc.motorTorque = wc.motorTorque * 0.1f;
                    }
                }
            }

            //handle horizontal (turning) movement
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

        }


        private void Awake()
        {
            gameObject.GetComponent<Animator>().enabled = false;
        }
    }
}