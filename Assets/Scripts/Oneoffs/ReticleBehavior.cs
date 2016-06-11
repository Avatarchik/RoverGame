using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class ReticleBehavior : MonoBehaviour
    {
        public Image reticleImage;
        public Image interactibleImage;

        public Color activeReticleColor;

        private PlayerStats cachedPlayerStats;
        private Color cachedReticleColor;
        private Color cachedInteractibleColor;

        public PlayerStats CachedPlayerStats
        {
            get { return (cachedPlayerStats != null) ? cachedPlayerStats : cachedPlayerStats = GameManager.Get<PlayerStats>();  }
        }


        public void ToggleInteractibleImage(bool setActive)
        {
            if(setActive)
                interactibleImage.color = activeReticleColor;
            else
                interactibleImage.color = cachedInteractibleColor;
        }


        private void Update()
        {
            if(CachedPlayerStats.movementEnabled != 0)
            {
                if(reticleImage.color != Color.clear)
                {
                    reticleImage.color = Color.clear;
                    interactibleImage.gameObject.SetActive(false);
                }
            }
            else if(reticleImage.color != cachedReticleColor)
            {
                reticleImage.color = cachedReticleColor;
                interactibleImage.gameObject.SetActive(true);
            }
        }


        private void Awake()
        {
            cachedReticleColor = reticleImage.color;
            cachedInteractibleColor = interactibleImage.color;
        }
    }
}