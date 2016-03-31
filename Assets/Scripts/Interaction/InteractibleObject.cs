using UnityEngine;
using System.Collections;

namespace Sol
{
    [System.Serializable]
    public class SoundControls
    {
        public enum PlayType
        {
            PlayAll,
            PlayRandom
        }

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

        private PlayerStats playerStats;

        private PlayerStats PlayerStats
        {
            get
            {
                if (playerStats == null) playerStats = GameManager.Get<PlayerStats>();
                if (playerStats == null) playerStats = GameObject.FindObjectOfType<PlayerStats>();
                return playerStats;
            }
        }

        public bool Interactible
        {
            //TODO we need a clearer reference to the players stats!
            get { return interactible && PlayerStats.movementEnabled == 0; }
        }


        public virtual void HoverEnterSeen()
        {
            if(Interactible)
            {
                silhouetteSeen.SetActive(true);
            }
        }


        public void HoverEnterInteractible()
        {
            if(Interactible)
            {
                silhouetteInteractible.SetActive(true);
                if (objectName != "")
                {
                    MessageMenu messageMenu = UIManager.GetMenu<MessageMenu>();
                    messageMenu.Open(objectName);
                }
            }
        }


        public virtual void HoverExitSeen()
        {
            if(Interactible)
            {
                silhouetteSeen.SetActive(false);
            }
        }


        public virtual void HoverExitInteractible()
        {
            if(Interactible)
            {
                if(silhouetteInteractible != null) silhouetteInteractible.SetActive(false);
                UIManager.Close<MessageMenu>();
            }
        }


        public virtual void Interact()
        {
            if(Interactible)
            {
                if (soundControls.interactEffects.Length > 0)
                {
                    SoundManager sm = GameManager.Get<SoundManager>();
                    switch (soundControls.soundPlayType)
                    {
                        case SoundControls.PlayType.PlayAll:
                            foreach(AudioClip ac in soundControls.interactEffects)
                            {
                                sm.Play(ac);
                            }
                            break;

                        case SoundControls.PlayType.PlayRandom:
                            int clipIndex = Mathf.RoundToInt(Random.Range(0, soundControls.interactEffects.Length));
                            sm.Play(soundControls.interactEffects[clipIndex]);
                            break;
                    }

                    if(soundControls.soundPlayType == SoundControls.PlayType.PlayAll)
                    {

                    }
                    else if(soundControls.soundPlayType == SoundControls.PlayType.PlayRandom)
                    {
                        
                    }
                }

                Debug.Log("interacting");
                UIManager.Close<MessageMenu>();
                silhouetteSeen.SetActive(false);
                silhouetteInteractible.SetActive(false);
            }
        }
    }
}