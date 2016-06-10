using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class PuzzleMenu : Menu
    {
        public Button exitButton;
        public Text messageText;

        //Different Wire Types
        public Ingredient AluminumWire;
		public Ingredient CopperWire;
		public Ingredient GoldWire;
		public Ingredient SilverWire;

        [HideInInspector]
        public InteractiblePuzzle currentPuzzleObject = null;

        private PuzzleManager cachedPuzzleManager;


        public PuzzleManager CachedPuzzleManager
        {
            get { return (cachedPuzzleManager != null) ? cachedPuzzleManager : cachedPuzzleManager = GameObject.FindObjectOfType<PuzzleManager>(); }
        }

		public void SetInitialWireCounts(){
			foreach (WireInventoryCount counter in gameObject.GetComponentsInChildren<WireInventoryCount>()) {
				if (counter.myWireType == WireInventoryCount.WireType.Aluminum) {
					int wireCount = UIManager.GetMenu<Inventory> ().GetIngredientAmount (AluminumWire);
					counter.SetWireCount (wireCount);
				} else if (counter.myWireType == WireInventoryCount.WireType.Copper) {
					int wireCount = UIManager.GetMenu<Inventory> ().GetIngredientAmount (CopperWire);
					counter.SetWireCount (wireCount);
				} else if (counter.myWireType == WireInventoryCount.WireType.Silver) {
					int wireCount = UIManager.GetMenu<Inventory> ().GetIngredientAmount (SilverWire);
					counter.SetWireCount (wireCount);
				} else if (counter.myWireType == WireInventoryCount.WireType.Gold) {
					int wireCount = UIManager.GetMenu<Inventory> ().GetIngredientAmount (GoldWire);
					counter.SetWireCount (wireCount);
				}
			}
		}

        public void Open(InteractiblePuzzle ip)
        {
            messageText.text = ip.message;
			if (exitButton == null) {
				foreach (Button child in ip.myPuzzleCanvas.GetComponentsInChildren<Button>(true)) {
					if (child.tag == "ExitButton") {
						exitButton = child;
					}
				}
				exitButton.onClick.AddListener (Exit);
			}
            currentPuzzleObject = ip;
			ActivateExit (true);
            base.Open();
        }


        public override void Close()
        {
            currentPuzzleObject.UiEvents.MissionButtonEvent(null);
            currentPuzzleObject.UiEvents.LevelButtonEvent(null);

			ActivateExit (false);

            currentPuzzleObject.interactible = true;
            base.Close();
        }


        public void Close(bool completed)
        {
            if(completed)
            {
                currentPuzzleObject.Complete = true;
				currentPuzzleObject.interactible = false;
				ActivateExit (false);
				exitButton = null;
				base.Close ();
            }
            else
            {
				Close ();
            }
        }


        public void Exit()
        {
            currentPuzzleObject.interactible = true;
            CachedPuzzleManager.Close();
            Close();
        }


		public void ActivateExit (bool active) {
			exitButton.transform.parent.gameObject.SetActive (active);
		}
    }
}
