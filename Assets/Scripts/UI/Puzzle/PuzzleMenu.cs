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
			foreach (WireInventoryCount counter in gameObject.GetComponentsInChildren<WireInventoryCount>(true)) {
				if (counter.myWireType == WireInventoryCount.WireType.Aluminum) {
					int wireCount = UIManager.GetMenu<Inventory> ().GetIngredientAmount (AluminumWire);
					counter.SetWireCount (wireCount);
					counter.myIngredient = AluminumWire;
				} else if (counter.myWireType == WireInventoryCount.WireType.Copper) {
					int wireCount = UIManager.GetMenu<Inventory> ().GetIngredientAmount (CopperWire);
					counter.SetWireCount (wireCount);
					counter.myIngredient = CopperWire;
				} else if (counter.myWireType == WireInventoryCount.WireType.Silver) {
					int wireCount = UIManager.GetMenu<Inventory> ().GetIngredientAmount (SilverWire);
					counter.SetWireCount (wireCount);
					counter.myIngredient = SilverWire;
				} else if (counter.myWireType == WireInventoryCount.WireType.Gold) {
					int wireCount = UIManager.GetMenu<Inventory> ().GetIngredientAmount (GoldWire);
					counter.SetWireCount (wireCount);
					counter.myIngredient = GoldWire;
				}
			}
		}

		public void SetWireText (Ingredient gridIngredient, bool scaleUp) {
			foreach (WireInventoryCount counter in gameObject.GetComponentsInChildren<WireInventoryCount>()) {
				if (counter.myIngredient == gridIngredient) {
					if (scaleUp) {
						counter.SetTextSizeUp ();
					} else {
						counter.SetTextSizeDown ();
					}
				}
			}
		}

        public void Open(InteractiblePuzzle ip)
        {
            Debug.Log("opening puzzle menu for some reason??");
            messageText.text = ip.message;
			foreach (Button child in ip.myPuzzleCanvas.GetComponentsInChildren<Button>(true)) {
				if (child.tag == "ExitButton") {
					exitButton = child;
				}
			}
			exitButton.onClick.AddListener (Exit);
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
