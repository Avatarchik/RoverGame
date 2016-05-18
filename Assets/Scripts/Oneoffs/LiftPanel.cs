using UnityEngine;
using System.Collections;

namespace Sol
{
    public class LiftPanel : InteractibleObject
    {
        public Lift controlledLift;


        public override void Interact()
        {
            base.Interact();

            if (Interactible)
            {
                controlledLift.MoveLift();
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