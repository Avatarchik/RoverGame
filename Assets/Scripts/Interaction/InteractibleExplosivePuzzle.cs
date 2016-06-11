using UnityEngine;
using System.Collections;

namespace Sol{
	public class InteractibleExplosivePuzzle : InteractiblePuzzle
    {
		public GameObject explosiveDevice;
		public Ingredient desiredIngredient;
		public bool triggered;
		public string failString = "You will need an {0} to clear this landslide";


		public override void Interact(){
			Inventory inventory = UIManager.GetMenu<Inventory>();
			MessageMenu messageMenu = UIManager.GetMenu<MessageMenu>();

			if (inventory.GetIngredientAmount (desiredIngredient) > 0) {
				triggered = true;
				inventory.RemoveInventoryItem (desiredIngredient, 1);
				explosiveDevice.SetActive(true);
				base.Interact ();
			} else if (!triggered) {
				interactible = false;
				failString = string.Format (failString, desiredIngredient);
				messageMenu.Open (failString, 4, 2.0f);

			} else if (interactible){
				base.Interact ();
			}
		}

		void InitiatePuzzle(){
			base.InitiatePuzzle ();
		}

		void Update() {
			base.Update ();
		}

		
		public override bool Complete {
			get {
				return base.Complete;
			}
			set {
				base.Complete = value;
				if (complete) {
					gameObject.GetComponent<CraterExplosion>().Detonate ();
					myPuzzleCanvas.GetComponent<Animator> ().SetTrigger ("FadeBackward");
				}
			}
		}
	}
}
