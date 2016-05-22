using UnityEngine;
using System.Collections;

namespace Sol{
	public class InteractibleExplosivePuzzle : InteractiblePuzzle
    {
		public Ingredient desiredIngredient;
		public bool triggered;
		public string failString = "You will need an {0} to clear this landslide";

		public override void Interact(){
			Debug.Log("Interacting");
			Inventory inventory = UIManager.GetMenu<Inventory>();
			MessageMenu messageMenu = UIManager.GetMenu<MessageMenu>();

			if (inventory.GetIngredientAmount (desiredIngredient) > 0) {
				Debug.Log ("wakka wakka");
				triggered = true;
				inventory.RemoveInventoryItem (desiredIngredient, 1);
				base.Interact ();
			} else if (!triggered) {
				interactible = false;
				messageMenu.Open (failString);

			} else if (interactible){
				base.Interact ();
			}
		}
		
		public override bool Complete {
			get {
				return base.Complete;
			}
			set {
				base.Complete = value;
				if (complete) {
					gameObject.GetComponent<CraterExplosion>().Detonate ();
				}
			}
		}
	}
}
