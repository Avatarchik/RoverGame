using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class Lift : InteractibleLerp
    {
        public AudioClip liftStop;

        private IEnumerator Load()
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
            while (async.progress < 0.9f)
            {
                yield return null;
            }
        }

        protected override IEnumerator Lerp()
        {
            SoundManager sm = GameManager.Get<SoundManager>();
            Transform player = GameObject.FindObjectOfType<PlayerStats>().transform;
            Transform cachedParent = player.parent;
            player.SetParent(controlledObject.transform);

            interactible = false;
            float elapsedTime = 0f;

            Vector3 startPos = controlledObject.position;
            Quaternion startRotation = controlledObject.rotation;

            Transform desiredPos = (triggered) ? pos1 : pos2;
            triggered = !triggered;

            List<SoundSource> sources = new List<SoundSource>();
            SoundSource ss = sm.Play(soundControls.interactEffects[0]);
            sources.Add(ss);

            for(int i = 1; i < soundControls.interactEffects.Length; i++)
            {
                sources.Add(sm.Play(soundControls.interactEffects[i]));
            }

            StartCoroutine(Load());

            while (elapsedTime < lerpTime)
            {
                controlledObject.position = Vector3.Lerp(startPos, desiredPos.position, elapsedTime / lerpTime);
                controlledObject.rotation = Quaternion.Lerp(startRotation, desiredPos.rotation, elapsedTime / lerpTime);

                elapsedTime += Time.fixedDeltaTime;
                yield return null;
            }

            foreach(SoundSource source in sources)
            {
                if(source != null) Destroy(source.gameObject);
            }

            sm.Play(liftStop);

            silhouetteSeen.SetActive(false);
            silhouetteInteractible.SetActive(false);
           
            player.SetParent(cachedParent);

            Destroy(this);
        }
    }
}

