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

        public MouseLook mouseLook;

        public Light flashLight;

        public bool strafing = true;
        public float movementSpeedMultiplier = 1.75f;

        public Animator wheelAnimator;
        public AnimationState animState;

        public List<WheelCollider> frontWheels = new List<WheelCollider>();
        public List<WheelCollider> backWheels = new List<WheelCollider>();
        public CarController carController;

        private PlayerStats cachedPlayerStats;

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


        public PlayerStats CachedPlayerStats
        {
            get { return (cachedPlayerStats != null) ? cachedPlayerStats : cachedPlayerStats = GameManager.Get<PlayerStats>(); }
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

            float h = 0; 
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");

            if (v != 0)
            {
                h = CrossPlatformInputManager.GetAxis("Horizontal");
                if (cachedSoundSource == null) cachedSoundSource = CachedSoundManager.Play(movementEffect);
            }
            else if (CrossPlatformInputManager.GetAxis("Horizontal") != 0)
            {
                float rotationAngle = CachedPlayerStats.MovementSpeed * movementSpeedMultiplier * Time.deltaTime * CrossPlatformInputManager.GetAxis("Horizontal");
                transform.Rotate(Vector3.up, rotationAngle);
                if (cachedSoundSource == null) cachedSoundSource = CachedSoundManager.Play(movementEffect);
            }
            else
            {
                if (cachedSoundSource != null)
                {
                    CachedSoundManager.Play(stopMovementEffect);
                    CachedSoundManager.Stop(cachedSoundSource);
                }
            }

            if(mouseLook.rotationX != 0 && h == 0 && v != 0)
            {
                //if the camera rotation is different than the chassi rotation and theres no horizontal input
                //then we want to turn anyway
                mouseLook.rotationX = Mathf.Lerp(mouseLook.rotationX, 0, Time.deltaTime);
                h = (mouseLook.rotationX / mouseLook.maximumX) * mouseLook.sensitivityX;
            }

            if (h != 0 && v >= 0)
                v = Mathf.Abs(h);
            else if (h != 0 && v <= 0)
                v = Mathf.Abs(h) * -1;

            wheelAnimator.SetFloat("speed", v);

            handbrake = (v == 0 && handbrake == 0) ? 1 : 0;

            carController.Move(h, v, v, handbrake);
        }


        private void Awake()
        {
            gameObject.GetComponent<Animator>().enabled = false;
        }
    }

}
