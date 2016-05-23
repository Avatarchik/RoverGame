using UnityEngine;
using System.Collections;

namespace Sol
{
    public class PuzzleTrigger : QuestTrigger
    {
        public override void CompleteObjective()
        {
            Debug.Log("completing puzzle objective!!");
            base.CompleteObjective();
        }


        private void Awake()
        {
            InteractiblePuzzle.onPuzzleComplete += CompleteObjective;
        }
    }
}

