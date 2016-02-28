using UnityEngine;
using System.Collections;

namespace Sol
{
    public class DrillPuzzleInitializer : InteractibleObject
    {
        public override void Interact()
        {
            base.Interact();
            UIManager.Open<FuelCellPuzzle>();
        }
    }
}

