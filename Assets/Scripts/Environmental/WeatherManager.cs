using UnityEngine;
using System.Collections;

namespace Sol
{
    [System.Serializable]
    public class Effect
    {
        public EllipsoidParticleEmitter effect;
        public ParticleAnimator particleAnimator;
        public Vector3 Offset = Vector3.zero;
        public bool fogDriven = false;
    }


    public class WeatherManager : MonoBehaviour
    {
        public Effect sandStorm;
        public Transform player;

        public void StartEffect(EllipsoidParticleEmitter effect)
        {
            effect.emit = true;
        }


        public void StopEffect(EllipsoidParticleEmitter effect)
        {
            effect.emit = false;
        }


        private void Update()
        {
            sandStorm.effect.transform.position = new Vector3(player.position.x + sandStorm.Offset.x, player.position.y + sandStorm.Offset.y, player.position.z + sandStorm.Offset.z);

            if (sandStorm.fogDriven)
            {
                Color[] colors = new Color[sandStorm.particleAnimator.colorAnimation.Length];
                Color fogColor = RenderSettings.fogColor;
                for (int i = 0; i < sandStorm.particleAnimator.colorAnimation.Length; i++)
                {
                    colors[i] = new Color(fogColor.r, fogColor.g, fogColor.b, sandStorm.particleAnimator.colorAnimation[i].a);
                }
                sandStorm.particleAnimator.colorAnimation = colors;
            }
        }
    }
}
