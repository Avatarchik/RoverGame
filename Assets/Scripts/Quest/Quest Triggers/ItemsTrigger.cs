using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class ItemsTrigger : QuestTrigger
    {
        public List<Ingredient> desiredItems = new List<Ingredient> ();
        public List<int> desiredItemCounts = new List<int> ();


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

            bool proceed = true;
            while(true)
            {
                proceed = true;
                for (int i = 0; i < desiredItems.Count; i++)
                {
                    if (inventory.GetIngredientAmount(desiredItems[i]) < desiredItemCounts[i]) proceed = false;
                }

                if (proceed) goto Completed;
                yield return null;
            }

            Completed:
            CompleteObjective();
        }
    }
}
