using UnityEngine;
using System.Collections;

namespace Sol
{
    public class InteractTrigger : QuestTrigger
    {
        public override void Initialize()
        {
            base.Initialize();
            interactible = true;
        }


        public override void Interact()
        {
            if (interactible) CompleteObjective();
        }
    }
}