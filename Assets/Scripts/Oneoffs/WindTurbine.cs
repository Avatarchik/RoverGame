using UnityEngine;
using System.Collections;

namespace Sol
{
    public class WindTurbine : MonoBehaviour
    {
        public enum TurbineBehavior { Rotate, Still }
        private const string ANIMATION_STATE = "randomnumber01";

        public TurbineBehavior currentTurbineBehavior = TurbineBehavior.Rotate;

        public Animator anim;

        public int minAnimationState = 1;
        public int maxAnimationState = 4;


        public float minTransitionDelay = 1f;
        public float maxTransitionDelay = 5f;


        public TurbineBehavior CurrentTurbineBehavior
        {
            get { return currentTurbineBehavior; }
            set
            {
                currentTurbineBehavior = value;

                if (currentTurbineBehavior == TurbineBehavior.Rotate)
                {
                    StartCoroutine(UpdateAnimationState());
                }
                else
                {
                    StopAllCoroutines();
                    anim.SetInteger(ANIMATION_STATE, minAnimationState);
                }
            }
        }


        private IEnumerator UpdateAnimationState()
        {
            anim.SetInteger(ANIMATION_STATE, Mathf.RoundToInt(Random.Range(minAnimationState, maxAnimationState)));

            yield return new WaitForSeconds(Random.Range(minTransitionDelay, maxTransitionDelay));

            StartCoroutine(UpdateAnimationState());
        }


        private void Awake()
        {
            if (anim == null) anim = GetComponentInChildren<Animator>();

            if(currentTurbineBehavior != TurbineBehavior.Still) StartCoroutine(UpdateAnimationState());
        }
    }
}

