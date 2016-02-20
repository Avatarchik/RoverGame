using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class ContainerObject : InteractibleObject
    {
        public List<Ingredient> ingredientsInInventory = new List<Ingredient>();
        private Container containerMenu;

        private Container ContainerMenu
        {
            get { return (containerMenu != null) ? containerMenu : containerMenu = UIManager.GetMenu<Container>(); }
        }


        public void AddIngredient(Ingredient ingredient)
        {
            ingredientsInInventory.Add(ingredient);
        }


        public void RemoveIngredient(Ingredient ingredient)
        {
            for (int i = 0; i < ingredientsInInventory.Count; i++)
            {
                if (ingredientsInInventory[i] == ingredient)
                {
                    ingredientsInInventory.RemoveAt(i);
                    break;
                }
            }
        }


        public override void Interact()
        {
            if (Interactible) ContainerMenu.Open(ingredientsInInventory, this);
        }
    }
}