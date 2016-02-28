using UnityEngine;
using System.Collections;

namespace Sol
{
    public class QuestTrigger : MonoBehaviour
    {
        public virtual void CompleteObjective()
        {
            GameManager.Get<QuestManager>().CurrentQuest.CompleteObjective();
        }
    }
}