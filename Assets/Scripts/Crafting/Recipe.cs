using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Recipe : ScriptableObject
{
    public int id;
    public string displayName;
    public string description;
    public Sprite image;
    public Ingredient craftedItem;

    public List<RecipePortion> requiredIngredients = new List<RecipePortion>();
}
