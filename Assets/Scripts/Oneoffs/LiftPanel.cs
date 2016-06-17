using UnityEngine;
using System.Collections;

namespace Sol
{
    public class LiftPanel : InteractibleObject
    {
        public Lift controlledLift;
		public Animator liftCanvasAnim;
		public bool withinDistance;
		public float playerDistance;
		public float maxInteractDistance;


        public override void Interact()
        {
            base.Interact();

			if (Interactible && withinDistance)
            {
                controlledLift.MoveLift();
				liftCanvasAnim.speed = 2.0f;
				liftCanvasAnim.SetTrigger ("FadeBackward");
                interactible = false;
            }
        }

		void Update() {
			playerDistance = Mathf.Abs (Vector3.Distance (transform.position, Camera.main.transform.position));
			if (playerDistance <= maxInteractDistance) {
				withinDistance = true;
			} else {
				withinDistance = false;
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