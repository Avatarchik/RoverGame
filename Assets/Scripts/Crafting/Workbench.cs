using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class Workbench : InteractibleObject
    {
        public List<Recipe> recipes = new List<Recipe>();


        public override void Interact()
        {
            if (Interactible)
            {
                Crafting craftingMenu = UIManager.GetMenu<Crafting>();

                silhouetteInteractible.SetActive(false);
                UIManager.Close<MessageMenu>();
                craftingMenu.Open(recipes);
            }
        }
    }

}
