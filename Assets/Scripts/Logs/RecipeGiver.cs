using UnityEngine;
using System.Collections;

namespace Sol
{
    public class RecipeGiver : InteractibleObject
    {
        public Recipe recipe;

        public override void Interact()
        {
            Debug.Log("attempting to interact");

            Debug.Log("giving player the recipe");
            PlayerStats playerStats = GameManager.Get<PlayerStats>();
            if (playerStats == null) playerStats = GameObject.FindObjectOfType<PlayerStats>();
            playerStats.AddRecipe(recipe);

        }
    }
}