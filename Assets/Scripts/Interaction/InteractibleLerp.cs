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

        protected bool triggered = false;


        public override void Interact()
        {
            StopAllCoroutines();
            StartCoroutine(Lerp());
        }


        protected virtual IEnumerator Lerp()
        {
            interactible = false;
            float elapsedTime = 0f;

            Vector3 startPos = controlledObject.position;
            Quaternion startRotation = controlledObject.rotation;

            Transform desiredPos = (triggered) ? pos1 : pos2;
            triggered = !triggered;

            while(elapsedTime < lerpTime)
            {
                controlledObject.position = Vector3.Lerp(startPos, desiredPos.position, elapsedTime/lerpTime);
                controlledObject.rotation = Quaternion.Lerp(startRotation, desiredPos.rotation, elapsedTime / lerpTime);

                elapsedTime += Time.fixedDeltaTime;
                yield return null;
            }

            silhouetteSeen.SetActive(false);
            silhouetteInteractible.SetActive(false);

            Destroy(this);
        }
    }
}