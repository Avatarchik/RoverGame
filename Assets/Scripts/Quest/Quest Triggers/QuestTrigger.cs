using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class QuestTrigger : MonoBehaviour
    {
        public delegate void CompleteObjectiveEvent(bool endQuest, int targetQuest = 0);
        public static event CompleteObjectiveEvent onCompleteObjective;

        public int targetQuest = 0;
        public bool endQuest = false;

        public List<GameObject> triggerObjects = new List<GameObject>();

        public bool initialized = false;

        private bool proceed = false;

        public virtual void  Initialize()
        {
            initialized = true;
            proceed = false;
            foreach(GameObject go in triggerObjects)
            {
                go.SetActive(true);
            }

            StartCoroutine(ProceedCoroutine());
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
            Debug.Log("calling complete!");
            if(initialized)
            {
                initialized = false;
                proceed = true;
            }
        }


        private IEnumerator ProceedCoroutine()
        {
            ObjectiveTracker ot = UIManager.GetMenu<ObjectiveTracker>();

            while(ot.IsActive || !proceed)
            {
                yield return null;
            }
            proceed = false;
            initialized = false;
            Debug.Log("finishing objective!");
            onCompleteObjective(endQuest, targetQuest);
        }
    }
}