using UnityEngine;
using System.Collections;

[System.Serializable]
public class InventoryIngredient
{
    public Ingredient ingredient;
    public int amount = 0;

    public void Add(int i)
    {
        Debug.Log("before : " + amount);
        amount += i;
        Debug.Log("after : " + amount);
    }
}
