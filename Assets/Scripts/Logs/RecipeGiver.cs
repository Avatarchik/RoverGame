using UnityEngine;
using System.Collections;

public class RecipeGiver : InteractibleObject
{
    public Recipe recipe;

    public override void Interact()
    {
        Debug.Log("attempting to interact");
        if(Interactible)
        {
            Debug.Log("giving player the recipe");
            PlayerStats playerStats = GameManager.Get<PlayerStats>();
            if (playerStats == null) playerStats = GameObject.FindObjectOfType<PlayerStats>();
            playerStats.AddRecipe(recipe);
        }
    }
}
