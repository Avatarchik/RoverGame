using UnityEngine;
using System.Collections;

namespace Sol
{
    public class PuzzleTrigger : QuestTrigger
    {
        private void Awake()
        {
            InteractiblePuzzle.onPuzzleComplete += CompleteObjective;
        }
    }
}

