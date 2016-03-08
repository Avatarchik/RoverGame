using UnityEngine;
using System.Collections;

namespace Sol
{
    public class DrillPuzzleInitializer : InteractibleObject
    {
        public Transform cameraPos;
        public GameObject leftFuelCell;
        public GameObject rightFuelCell;

        public ContainerObject containerObj;

        public override void Interact()
        {
            base.Interact();
            FuelCellPuzzle fcp = UIManager.GetMenu<FuelCellPuzzle>();
            fcp.Open(leftFuelCell, rightFuelCell, cameraPos, containerObj);
        }
    }
}

