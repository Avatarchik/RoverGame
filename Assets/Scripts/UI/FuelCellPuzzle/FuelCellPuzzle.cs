using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class FuelCellPuzzle : Menu
    {
        public Ingredient reward;

        public Image leftCell;
        public Image rightCell;

        public ChargeTracker rightCharger;
        public ChargeTracker leftCharger;

        public Button exitButton;

        public int desiredCharge = 5;

        private bool isComplete = false;


        public void CheckCompletion()
        {
            if(!isComplete && rightCharger.CellCurrent == desiredCharge)
            {
                isComplete = true;
                //GameManager.Get<QuestManager>().CurrentQuest.CompleteObjective();
                UIManager.GetMenu<Inventory>().AddInventoryItem(reward, 1);
                Close();
                this.enabled = false;

                GameObject.FindObjectOfType<DrillPuzzleInitializer>().enabled = false;
            }
        }


        private void Awake()
        {
            exitButton.onClick.AddListener(Close);
        }
    }
}

