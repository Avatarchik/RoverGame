using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Workbench : InteractibleObject
{
    public List<Recipe> recipes = new List<Recipe>();


    public override void Interact()
    {
        if (Interactible)
        {
            Crafting craftingMenu = UIManager.GetMenu<Crafting>();

            silhouette.SetActive(false);
            UIManager.Close<MessageMenu>();
            craftingMenu.recipes = recipes;
            craftingMenu.Open();
        }
    } 
}
