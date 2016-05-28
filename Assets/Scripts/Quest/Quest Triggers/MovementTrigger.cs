using UnityEngine;
using System.Collections;

namespace Sol
{
    public class MovementTrigger : QuestTrigger
    {
        public override void Initialize()
        {
            base.Initialize();
            WaitCheck();
        }


        public void WaitCheck()
        {
            StartCoroutine(WaitForMovement());
        }


        private IEnumerator WaitForMovement()
        {
            while(Input.GetAxis("Mouse X") == 0 && 
                Input.GetAxis("Mouse Y") == 0)
            {
                yield return null;
            }

            CompleteObjective();
        }
    }
}