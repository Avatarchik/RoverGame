using UnityEngine;
using System.Collections;

namespace Sol
{
    public class CameraDriver : MonoBehaviour
    {
        private const int ROVER_MOVEMENT_SOUND_ID = 20;

        public Camera playerCamera;
        public Transform model;

        public float minX = -360.0f;
        public float maxX = 360.0f;

        public float minY = -45.0f;
        public float maxY = 45.0f;

        public float sensX = 100.0f;
        public float sensY = 100.0f;

        public float minZoom = 10;
        public float maxZoom = 70;

        public float zoomSpeed = 20.0f;

        private float rotationY = 0.0f;
        private float rotationX = 0.0f;

        private float rotationYL = 0.0f;
        private float rotationXL = 0.0f;

        private float zoom;

        private PlayerStats playerStats;
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
            if (playerStats.TurnSpeed > 0)
            {
                rotationX += Input.GetAxis("Mouse X") * sensX * Time.deltaTime * playerStats.TurnSpeed;
                rotationY += Input.GetAxis("Mouse Y") * sensY * Time.deltaTime * playerStats.TurnSpeed;
                rotationY = Mathf.Clamp(rotationY, minY, maxY);

                if (rotationY != rotationYL || rotationX != rotationXL)
                {
                    //TODO get rid of magic numbers
                    //playerCamera.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
                  //  playerCamera.transform.Rotate(Vector3.right, playerStats.MoveSpeed * Time.fixedDeltaTime * Input.GetAxis("Mouse Y") * playerStats.TurnSpeed * -20f);
                    transform.Rotate(Vector3.up, playerStats.MoveSpeed * Time.fixedDeltaTime * Input.GetAxis("Mouse X") * playerStats.TurnSpeed * 20f);

                    //if (!CachedSoundManager.IsPlaying(cachedSoundManager.Find(ROVER_MOVEMENT_SOUND_ID)))
                      //  CachedSoundManager.Play(ROVER_MOVEMENT_SOUND_ID);
                }
                else
                {
                  //  CachedSoundManager.Stop(ROVER_MOVEMENT_SOUND_ID);
                }

                rotationXL = rotationX;
                rotationYL = rotationY;

                playerCamera.fieldOfView = Mathf.Clamp(playerCamera.fieldOfView + (Input.GetAxis("Mouse ScrollWheel") * -zoomSpeed), minZoom, maxZoom);
            }
        }


        private void Awake()
        {
            playerStats = GameObject.FindObjectOfType<PlayerStats>() as PlayerStats;
        }
    }

}
