using UnityEngine;
using System.Collections;

namespace Sol
{
    public class LiftPanel : InteractibleObject
    {
        public Lift controlledLift;
		public Animator liftCanvasAnim;


        public void ActivateLift()
        {
			print ("DFDSF");
            base.Interact();
			if (interactible)
            {
				print ("SAD");
                controlledLift.MoveLift();
				print ("DSDSFDF");
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