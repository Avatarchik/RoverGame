using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class CraterExplosion : InteractibleObject
    {
        public delegate void ExplosiveEvent(bool landslide);
        public static event ExplosiveEvent OnExplosivePlaced;

        private const int EXPLOSION_SOUND_ID = 2091111392;

        public GameObject explosiveDevice;
        public GameObject explosionPrefab1;
        public GameObject explosionPrefab2;
        public Transform explosionOrigin;

        public int explosionDelay = 10;
        public float shakeIntensity = 1f;
        public float shakeDecay = 0.05f;

        public bool isLandslide = true;

        public List<GameObject> rockWall = new List<GameObject>();

        private bool triggered = false;

        public override void Interact()
        {
            
        }

		public void Detonate(){
			Debug.Log("DETONATE");
			StartCoroutine(DetonateDelay());
		}

        public void TriggerExplosion()
        {
            Intro intro = GameObject.FindObjectOfType<Intro>();
            CameraShake cameraShakeInstance = GameObject.FindObjectOfType<CameraShake>();
            GameManager.Get<SoundManager>().Play(EXPLOSION_SOUND_ID);

            GameObject explosion1 = Instantiate(explosionPrefab1, explosionOrigin.transform.position, explosionOrigin.transform.rotation) as GameObject;
            GameObject explosion2 = Instantiate(explosionPrefab2, explosionOrigin.transform.position, explosionOrigin.transform.rotation) as GameObject;
            explosion1.transform.SetParent(explosionOrigin);
            explosion2.transform.SetParent(explosionOrigin);

            cameraShakeInstance.Shake(shakeIntensity, shakeDecay);

            foreach (GameObject go in rockWall)
            {
                go.SetActive(false);
            }
        }


		public IEnumerator DetonateDelay()
        {
           
            explosiveDevice.SetActive(true);
            CountDown countDown = UIManager.GetMenu<CountDown>();
            countDown.SetText(explosionDelay);
            yield return new WaitForSeconds(explosionDelay);
            TriggerExplosion();
            explosiveDevice.SetActive(false);
			OnExplosivePlaced(isLandslide);
        }


        private IEnumerator DelayedClose()
        {
            yield return new WaitForSeconds(5f);
            UIManager.Close<MessageMenu>();
            interactible = true;
        }
    }
}