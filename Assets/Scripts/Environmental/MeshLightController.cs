using UnityEngine;
using System.Collections;

namespace Sol
{
    public class MeshLightController : MonoBehaviour
    {
        public LightController lightSource;

        public float maxEmission = 1.7f;

        public Material controlledMaterial;  


        private void FixedUpdate()
        {
            float desiredEmission = (lightSource.controlledLight.intensity * maxEmission) / lightSource.lightSettings.baseIntensity;
            Color currentColor = controlledMaterial.GetColor("_EmissionColor");
            controlledMaterial.SetColor("_EmissionColor", new Color(currentColor.r, currentColor.g, currentColor.b, desiredEmission));
        }
    }
}