using UnityEngine;
using System.Collections;

namespace Sol
{
    public class Lift : InteractibleLerp
    {
        protected override IEnumerator Lerp()
        {
            Transform player = GameObject.FindObjectOfType<PlayerStats>().transform;
            Transform cachedParent = player.parent;
            player.SetParent(controlledObject.transform);

            interactible = false;
            float elapsedTime = 0f;

            Vector3 startPos = controlledObject.position;
            Quaternion startRotation = controlledObject.rotation;

            Transform desiredPos = (triggered) ? pos1 : pos2;
            triggered = !triggered;

            while (elapsedTime < lerpTime)
            {
                controlledObject.position = Vector3.Lerp(startPos, desiredPos.position, elapsedTime / lerpTime);
                controlledObject.rotation = Quaternion.Lerp(startRotation, desiredPos.rotation, elapsedTime / lerpTime);

                elapsedTime += Time.fixedDeltaTime;
                yield return null;
            }

            silhouetteSeen.SetActive(false);
            silhouetteInteractible.SetActive(false);
           
            player.SetParent(cachedParent);

            Destroy(this);
        }
    }
}

