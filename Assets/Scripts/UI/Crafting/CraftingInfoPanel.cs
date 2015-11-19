using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CraftingInfoPanel : MonoBehaviour
{
    public Image image;
    public Text titleText;
    public Text descriptionText;
    public Button craftButton;
    public Transform requiredIngredientContainer;

    public RequiredIngredientSlot requiredIngredientSlotPrefab;

    private Recipe selectedRecipe;

    public List<RequiredIngredientSlot> spawnedRIS = new List<RequiredIngredientSlot>();

    
	
    public Recipe SelectedRecipe
    {
        get { return selectedRecipe; }
        set
        {
            selectedRecipe = value;
            image.sprite = selectedRecipe.image;
            titleText.text = selectedRecipe.displayName;
            descriptionText.text = selectedRecipe.description;

            for(int i = spawnedRIS.Count - 1; i >= 0; i--)
            {
                Destroy(spawnedRIS[i].gameObject);
            }

            spawnedRIS.Clear();

            foreach(RecipePortion rp in selectedRecipe.requiredIngredients)
            {
                RequiredIngredientSlot ris = Instantiate(requiredIngredientSlotPrefab);
                ris.transform.SetParent(requiredIngredientContainer);
                ris.transform.localScale = Vector3.one;

                ris.image.sprite = rp.ingredient.image;
                ris.amountText.text = rp.ingredientCount + "";

                spawnedRIS.Add(ris);
            }
        }
    }
}
