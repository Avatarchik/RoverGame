using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class QuestTrigger : MonoBehaviour
    {
        public delegate void CompleteObjectiveEvent(bool hasTargetQuest = false, int targetQuest = 0);
        public static event CompleteObjectiveEvent onCompleteObjective;

        public bool isDecision = false;
        public int targetQuest = 0;

        public List<GameObject> triggerObjects = new List<GameObject>();

        public bool initialized = false;

        public virtual void  Initialize()
        {
            initialized = true;

            foreach(GameObject go in triggerObjects)
            {
                go.SetActive(true);
            }
        }


        public virtual void Cleanup()
        {
            foreach (GameObject go in triggerObjects)
            {
                go.SetActive(false);
            }
        }


        public virtual void CompleteObjective()
        {
            if(initialized)
            {
                initialized = false;
                onCompleteObjective(isDecision, targetQuest);
            }
        }
    }
}