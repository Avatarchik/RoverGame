using UnityEngine;
using System.Collections;

namespace Sol
{
    public class ItemTrigger : QuestTrigger
    {
        public Ingredient desiredItem;
        public int desiredCount;


        public override void Initialize()
        {
            base.Initialize();
            ItemCheck();
        }


        public void ItemCheck()
        {
            StartCoroutine(ItemCheckCoroutine());
        }


        private IEnumerator ItemCheckCoroutine()
        {
            Inventory inventory = UIManager.GetMenu<Inventory>();

            while(inventory.GetIngredientAmount(desiredItem) < desiredCount)
            {
                yield return new WaitForSeconds(0.5f);
            }

            CompleteObjective();
        }
    }
}
