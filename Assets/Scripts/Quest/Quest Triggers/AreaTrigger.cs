using UnityEngine;
using System.Collections;

namespace Sol
{
    public class AreaTrigger : QuestTrigger
    {
        private void OnTriggerenter(Collider other)
        {
            if (other.gameObject.tag == "Player") CompleteObjective();
        }
    }
}

