using UnityEngine;
using System.Collections;

namespace Sol
{
    public class WaitTrigger : QuestTrigger
    {
        public void Wait(float time = 1f)
        {
            StartCoroutine(WaitCoroutine(time));
        }


        private IEnumerator WaitCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            CompleteObjective();
        }
    }
}

