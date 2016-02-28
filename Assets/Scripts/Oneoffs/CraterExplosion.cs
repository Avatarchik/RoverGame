using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class CraterExplosion : InteractibleObject
    {
        private const int EXPLOSION_SOUND_ID = 30;

        public GameObject explosiveDevice;
        public GameObject explosionPrefab1;
        public GameObject explosionPrefab2;
        public Transform explosionOrigin;
        public Ingredient desiredIngredient;
        public string failString = "You will need an {0} to clear this landslide";

        public int explosionDelay = 10;
        public float shakeIntensity = 1f;
        public float shakeDecay = 0.05f;

        public List<GameObject> rockWall = new List<GameObject>();

        private bool triggered = false;

        public override void Interact()
        {
            Debug.Log("Interacting");
            Inventory inventory = UIManager.GetMenu<Inventory>();
            MessageMenu messageMenu = UIManager.GetMenu<MessageMenu>();

            if (inventory.GetIngredientAmount(desiredIngredient) > 0)
            {
                Debug.Log("wakka wakka");
                triggered = true;
                inventory.RemoveInventoryItem(desiredIngredient, 1);
                StartCoroutine(DetonateDelay());
            }
            else if (!triggered)
            {
                StopAllCoroutines();
                interactible = false;
                messageMenu.Open(failString);

            }
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

            intro.NextObjective(intro.enterTunnelsObjective, true);
        }


        private IEnumerator DetonateDelay()
        {
            Debug.Log("delaying!");
            explosiveDevice.SetActive(true);
            CountDown countDown = UIManager.GetMenu<CountDown>();
            countDown.SetText(explosionDelay);
            yield return new WaitForSeconds(explosionDelay);
            TriggerExplosion();
            explosiveDevice.SetActive(false);
        }


        private IEnumerator DelayedClose()
        {
            yield return new WaitForSeconds(5f);
            UIManager.Close<MessageMenu>();
            interactible = true;
        }
    }
}