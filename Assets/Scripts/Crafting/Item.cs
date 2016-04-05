using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : Ingredient
{
    public Recipe recipe;
    public List<Modifier> statModifiers = new List<Modifier> ();

    private void Awake()
    {
        stackable = false;
    }
}
