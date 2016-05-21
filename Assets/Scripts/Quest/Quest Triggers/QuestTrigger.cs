using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class QuestTrigger : MonoBehaviour
    {
        public delegate void CompleteObjectiveEvent();
        public static event CompleteObjectiveEvent onCompleteObjective;

        public List<GameObject> triggerObjects = new List<GameObject>();

        private bool initialized = false;

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
                onCompleteObjective();
            }
        }
    }
}