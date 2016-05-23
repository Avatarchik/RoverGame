using UnityEngine;
using System.Collections;
namespace Sol
{
    public class InteractiblePickup :  InteractibleObject
    {
        public Ingredient myIngredient;

        public override void Interact()
        {
            base.Interact();
            UIManager.GetMenu<Inventory>().AddInventoryItem(myIngredient, 1);
            UIManager.Close<MessageMenu>();
            Destroy(gameObject);
        }
    }
}

