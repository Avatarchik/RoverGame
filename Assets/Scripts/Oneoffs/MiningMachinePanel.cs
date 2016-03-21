using UnityEngine;
using System.Collections;

namespace Sol
{
    public class MiningMachinePanel : InteractibleLerp
    {
        public GameObject puzzleInitializer;

        public override void Interact()
        {
            puzzleInitializer.SetActive(true);
            base.Interact();
        }
    }
}