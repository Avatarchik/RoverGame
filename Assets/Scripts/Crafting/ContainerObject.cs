using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContainerObject : InteractibleObject
{
    public Container containerMenu;

    public List<Ingredient> ingredientsInInventory = new List<Ingredient>();


    public void AddIngredient(Ingredient ingredient)
    {
        ingredientsInInventory.Add(ingredient);
    }


    public void RemoveIngredient(Ingredient ingredient)
    {
        for(int i = 0; i < ingredientsInInventory.Count; i++)
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
        //base.Interact();
        containerMenu.Open(ingredientsInInventory, this);
    }


    private void Awake()
    {
        if (containerMenu == null) containerMenu = GameObject.FindObjectOfType<Container>() as Container;
    }
}
