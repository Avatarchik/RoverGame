using UnityEngine;
using System.Collections;

public class RecipeGiver : InteractibleObject
{
    public Recipe recipe;

    public override void Interact()
    {
        if(Interactible)
        {
            Debug.Log("this is happening");
            PlayerStats playerStats = GameManager.Get<PlayerStats>();

            playerStats.AddRecipe(recipe);
        }
    }
}
