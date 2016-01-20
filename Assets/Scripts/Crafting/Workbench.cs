using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Workbench : InteractibleObject
{
    public Crafting craftingMenu;
    public List<Recipe> recipes = new List<Recipe>();

    public Crafting CraftingMenu
    {
        get { return (craftingMenu != null) ? craftingMenu : craftingMenu = GameObject.FindObjectOfType<Crafting>() as Crafting; }
    }


    public override void Interact()
    {
        if (Interactible) craftingMenu.Open();
    } 
}
