using UnityEngine;
using System.Collections;

namespace Sol
{
    public class CameraBumper : MonoBehaviour
    {
        public Transform target;
        public Transform followCamera;

        public float followSpeed = 1f;

        private float currentFollowSpeed = 1f;

        private void OnCollisionEnter(Collision collision)
        {
            currentFollowSpeed = 0.01f;

        }


        private void OnCollisionExit(Collision collision)
        {
            currentFollowSpeed = followSpeed;

        }


        private void FixedUpdate()
        {
            followCamera.position = Vector3.Lerp(target.position, followCamera.position, currentFollowSpeed * Time.fixedDeltaTime);
        }


        private void Awake()
        {
            currentFollowSpeed = followSpeed;
        }
    }
}
