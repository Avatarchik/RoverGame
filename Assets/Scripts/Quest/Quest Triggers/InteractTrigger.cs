using UnityEngine;
using System.Collections;

namespace Sol
{
    public class InteractTrigger : InteractibleObject
    {
        public override void Interact()
        {
            CompleteObjective();
        }

        public void CompleteObjective()
        {
            GameManager.Get<QuestManager>().CurrentQuest.CompleteObjective();
        }
    }
}