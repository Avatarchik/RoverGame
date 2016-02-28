using UnityEngine;
using System.Collections;

namespace Sol
{
    public class ItemTrigger : QuestTrigger
    {
        public Ingredient desiredItem;

        public void ItemCheck()
        {
            StartCoroutine(ItemCheckCoroutine());
        }

        private IEnumerator ItemCheckCoroutine()
        {
            Inventory inventory = UIManager.GetMenu<Inventory>();

            while(inventory.GetIngredientAmount(desiredItem) <= 0)
            {
                yield return new WaitForSeconds(0.5f);
                CompleteObjective();
            }
        }
    }
}
