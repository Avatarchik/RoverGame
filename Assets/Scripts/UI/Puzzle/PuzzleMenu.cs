using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class PuzzleMenu : Menu
    {
        public Button exitButton;
        public Text messageText;
        public Text wiresInInventory;
        public Text wiresUsed;
		public WireCountFill[] wireCounts;

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

		void Start(){
			wireCounts = transform.GetComponentsInChildren<WireCountFill> (true);
		}

		public void SetInitialWireCounts(){
			foreach (WireCountFill filler in wireCounts) {
				int initialCount = UIManager.GetMenu<Inventory> ().GetIngredientAmount (filler.wireIngredient);
				filler.SetWireCount (initialCount);
			}
		}

		public void SetCurrentWireCounts(Ingredient beginIngrdient, int currentCount){
			foreach (WireCountFill filler in wireCounts) {
				if (filler.wireIngredient == beginIngrdient) {
					filler.UpdateWireCount (currentCount);
				}
			}
		}


        public void Open(InteractiblePuzzle ip)
        {
            messageText.text = ip.message;
			if (exitButton == null) {
				foreach (Button child in ip.GetComponentsInChildren<Button>(true)) {
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
