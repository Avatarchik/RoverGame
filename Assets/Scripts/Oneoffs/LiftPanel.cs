using UnityEngine;
using System.Collections;

namespace Sol
{
    public class LiftPanel : InteractibleObject
    {
        public Lift controlledLift;
		public Animator liftCanvasAnim;


        public override void Interact()
        {
            base.Interact();

            if (Interactible)
            {
                controlledLift.MoveLift();
				liftCanvasAnim.speed = 2.0f;
				liftCanvasAnim.SetTrigger ("FadeBackward");
                interactible = false;
            }
        }


        private void ResetLiftPanel()
        {
            interactible = true;
        }


        private void Awake()
        {
            Lift.OnLiftStop += ResetLiftPanel;
        }
    }
}