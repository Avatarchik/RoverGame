using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Car;

namespace Sol
{
    public class CharacterDriver : MonoBehaviour
    {
        public AudioClip movementEffect;
        public AudioClip stopMovementEffect;

        public PlayerStats playerStats;

        public CameraDriver cameraDriver;

        public Light flashLight;

        public bool strafing = true;
        public float movementSpeedMultiplier = 1.75f;

        public List<WheelCollider> frontWheels = new List<WheelCollider>();
        public List<WheelCollider> backWheels = new List<WheelCollider>();
        public CarController carController;

        private bool flashLightActive = false;
        private SoundManager cachedSoundManager;

        private bool canFlipSpeed = false;
        private SoundSource cachedSoundSource = null;

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
            if (Input.GetKeyDown(KeyCode.F))
            {
                flashLightActive = !flashLightActive;
                flashLight.gameObject.SetActive(flashLightActive);
            }

            if (Input.GetKeyDown(KeyCode.R))Reset();
        }


        private void FixedUpdate()
        {
            if (carController.MaxSpeed != playerStats.CurrentMovementSpeed) carController.MaxSpeed = playerStats.CurrentMovementSpeed;

            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");

            handbrake = (v == 0 && handbrake == 0) ? 1 : 0;

            if (v != 0)
            {
                if(cachedSoundSource == null) cachedSoundSource = CachedSoundManager.Play(movementEffect);
            }
            else
            {
                if (cachedSoundSource != null)
                {
                    Debug.Log("stopping");
                    CachedSoundManager.Play(stopMovementEffect);
                    CachedSoundManager.Stop(cachedSoundSource);
                }
            }

            carController.Move(h, v, v, handbrake);
        }


        private void Awake()
        {
            gameObject.GetComponent<Animator>().enabled = false;
        }
    }

}
