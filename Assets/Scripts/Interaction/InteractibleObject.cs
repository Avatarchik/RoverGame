using UnityEngine;
using System.Collections;

namespace Sol
{
    [System.Serializable]
    public class SoundControls
    {
        public enum PlayType { PlayAll, PlayRandom }

        public PlayType soundPlayType = PlayType.PlayRandom;
        public AudioClip[] interactEffects;
    }

    public class InteractibleObject : MonoBehaviour
    {
        public SoundControls soundControls;

        public string objectName = "Container";

        public GameObject silhouetteInteractible;
        public GameObject silhouetteSeen;
        public bool interactible = true;

        protected PlayerStats playerStats;

        protected PlayerStats PlayerStats
        {
            get
            {
                if (playerStats == null) playerStats = GameManager.Get<PlayerStats>();
                return (playerStats != null) ? playerStats : playerStats = GameObject.FindObjectOfType<PlayerStats>();
            }
        }

        public bool Interactible
        {
            get { return interactible && PlayerStats.movementEnabled == 0; }
        }


        public virtual void HoverEnterSeen()
        {
            if (Interactible)if (silhouetteSeen != null) silhouetteSeen.SetActive(true);
        }


        public virtual void HoverEnterInteractible()
        {
            if (Interactible)
            {
                if (silhouetteInteractible != null) silhouetteInteractible.SetActive(true);
                if (objectName != "") UIManager.GetMenu<MessageMenu>().Open(objectName);
            }
        }


        public virtual void HoverExitSeen()
        {
            if (Interactible) if (silhouetteSeen != null) silhouetteSeen.SetActive(false);
        }


        public virtual void HoverExitInteractible()
        {
            if (Interactible)
            {
                if (silhouetteInteractible != null) silhouetteInteractible.SetActive(false);
                UIManager.Close<MessageMenu>();
            }
        }


        public virtual void Interact()
        {
            Debug.Log(0);
            if (Interactible)
            {
                Debug.Log(1);
                if (soundControls.interactEffects.Length > 0)
                {
                    Debug.Log(2);
                    SoundManager sm = GameManager.Get<SoundManager>();
                    switch (soundControls.soundPlayType)
                    {
                        case SoundControls.PlayType.PlayAll:
                            Debug.Log("playing all interact effects!");
                            foreach (AudioClip ac in soundControls.interactEffects) { sm.Play(ac); }
                            break;

                        case SoundControls.PlayType.PlayRandom:
                            Debug.Log("playing random interact effects!");
                            int clipIndex = Mathf.RoundToInt(Random.Range(0, soundControls.interactEffects.Length));
                            sm.Play(soundControls.interactEffects[clipIndex]);
                            break;
                    }
                }

                UIManager.Close<MessageMenu>();
                if(silhouetteSeen != null) silhouetteSeen.SetActive(false);
                if (silhouetteInteractible != null) silhouetteInteractible.SetActive(false);
            }
        }


        protected virtual IEnumerator DelayedInteract(float delay = 1f)
        {
            float elapsedTime = 0f;

            while(elapsedTime < delay)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Interact();
        }
    }
}