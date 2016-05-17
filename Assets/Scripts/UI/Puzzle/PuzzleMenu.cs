using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class PuzzleMenu : Menu
    {
        public const string WIRES_IN_INVENTORY_FORMAT = "Wires in Inventory : {0}";
        public const string WIRES_USED_FORMAT = "Wires Used : {0}";

        public Button exitButton;
        public Text messageText;
        public Text wiresInInventory;
        public Text wiresUsed;

        //TODO we need to support more than one kind of wire eventually
        public Ingredient AluminumWire;
		public Ingredient CopperWire;
		public Ingredient GoldWire;
		public Ingredient SilverWire;

        [HideInInspector]
        public InteractiblePuzzle currentPuzzleObject = null;

        private PuzzleManager cachedPuzzleManager;

        public void SetWiresUsed(int wires)
        {
            wiresUsed.text = string.Format(WIRES_USED_FORMAT, wires);
        }

        public PuzzleManager CachedPuzzleManager
        {
            get { return (cachedPuzzleManager != null) ? cachedPuzzleManager : cachedPuzzleManager = GameObject.FindObjectOfType<PuzzleManager>(); }
        }


        public void Open(InteractiblePuzzle ip)
        {
            messageText.text = ip.message;
            //TODO make this down here less gross
			wiresInInventory.text = string.Format(WIRES_IN_INVENTORY_FORMAT, UIManager.GetMenu<Inventory>().GetIngredientAmount(AluminumWire));
            currentPuzzleObject = ip;
            base.Open();
        }


        public override void Close()
        {
            Debug.Log("close");

            currentPuzzleObject.UiEvents.MissionButtonEvent(null);
            currentPuzzleObject.UiEvents.LevelButtonEvent(null);

            currentPuzzleObject.interactible = true;
            base.Close();
        }


        public void Close(bool completed)
        {
            if(completed)
            {
                currentPuzzleObject.Complete = true;
                base.Close();
            }
            else
            {
                base.Close();
            }
        }


        public void Exit()
        {
            Debug.Log("exit");
            currentPuzzleObject.interactible = true;
            CachedPuzzleManager.Close();
            Close();
        }


        public void Awake()
        {
            exitButton.onClick.AddListener(Exit);
        }
    }
}
