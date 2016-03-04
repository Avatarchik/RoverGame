using UnityEngine;
using System.Collections;

namespace Sol
{
    public class InteractibleLerp : InteractibleObject
    {
        public Transform pos1;
        public Transform pos2;

        public Transform controlledObject;

        public float lerpTime;

        private bool triggered = false;


        public override void Interact()
        {
            StopAllCoroutines();
            StartCoroutine(Lerp());
        }


        private IEnumerator Lerp()
        {
            interactible = false;
            float elapsedTime = 0f;

            Transform desiredPos = (triggered) ? pos1 : pos2;
            triggered = !triggered;

            while(elapsedTime < lerpTime)
            {
                controlledObject.position = Vector3.Lerp(controlledObject.position, desiredPos.position, elapsedTime/lerpTime);
                controlledObject.rotation = Quaternion.Lerp(controlledObject.rotation, desiredPos.rotation, elapsedTime / lerpTime);

                elapsedTime += Time.fixedDeltaTime;
                yield return null;
            }

            this.enabled = false;
        }
    }
}