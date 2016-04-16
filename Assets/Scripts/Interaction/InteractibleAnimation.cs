using UnityEngine;
using System.Collections;

namespace Sol
{
    public class InteractibleAnimation : InteractibleObject
    {
        public Animator animator;
        public string parameter = "Play";

        private bool opened = false;
        private bool forward = false;

        public override void Interact()
        {
            if(interactible)
            {
                animator.SetBool(parameter, true);

                StartCoroutine("Reset");
            }

            base.Interact();
        }

        private IEnumerator Reset()
        {
            yield return new WaitForSeconds(0.1f);
            animator.SetBool(parameter, false);
        }
    }
}