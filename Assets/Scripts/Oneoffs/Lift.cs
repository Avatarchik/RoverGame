using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class Lift : MonoBehaviour
    {
        public delegate void LiftEvent();
        public static event LiftEvent OnLiftStop;

        public SoundControls soundControls;

        public AudioClip liftStop;
        public Animator liftAnimator;

        public bool atTop = false;

        private Transform cachedParent;
        private Transform player;
        private List<SoundSource> sources = new List<SoundSource>();

        private Transform PlayerTransform
        {
            get { return (player != null) ? player : GameObject.FindObjectOfType<PlayerStats>().transform; }
        }

        public void MoveLift()
        {
            InitializeMovement();
            PlayLiftNoises();

            liftAnimator.SetBool("Activated", true);
            StartCoroutine(HandleAnimation());
        }


        private void InitializeMovement()
        {
            cachedParent = PlayerTransform.parent;
            PlayerTransform.SetParent(this.transform);
        }


        private void PlayLiftNoises()
        {
            SoundManager sm = GameManager.Get<SoundManager>();
            SoundSource ss = sm.Play(soundControls.interactEffects[0]);

            sources.Add(ss);

            for (int i = 1; i < soundControls.interactEffects.Length; i++)
            {
                sources.Add(sm.Play(soundControls.interactEffects[i]));
            }
        }


        private void StopLiftNoises()
        {
            SoundManager sm = GameManager.Get<SoundManager>();

            foreach (SoundSource source in sources)
            {
                if (source != null) source.Stop();
            }

            sm.Play(liftStop);
        }


        private IEnumerator HandleAnimation()
        {
            yield return null;
            
            liftAnimator.SetBool("Activated", false);

            yield return new WaitForSeconds(40f);

            StopLiftNoises();
            PlayerTransform.SetParent(cachedParent);
            OnLiftStop();
        }
    }
}

