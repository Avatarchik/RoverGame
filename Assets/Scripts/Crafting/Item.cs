using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : Ingredient
{
    public Recipe recipe;
    public Modifier statModifier;

    private void Awake()
    {
        stackable = false;
        equippable = true;
    }
}
