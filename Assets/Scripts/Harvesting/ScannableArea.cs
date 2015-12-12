using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScannableArea : MonoBehaviour
{
    public float harvestTime;

    public InventoryIngredient tier1Element;
    public InventoryIngredient tier2Element;
    public InventoryIngredient tier3Element;

    public float rareElementHarvestChance;
    public InventoryIngredient rareElement;

    private bool scannable = false;
    private Harvesting harvestingMenu;

    public bool Scannable
    {
        get { return scannable; }
        set
        {
            scannable = value;

            if (harvestingMenu == null) harvestingMenu = GameObject.FindObjectOfType<Harvesting>() as Harvesting;
            if(scannable)
            {
                harvestingMenu.OpenNote();
            }
            else
            {
                harvestingMenu.CloseNote();
            }
            
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") Scannable = true;
    }


    private void OnTriggerExit(Collider other)
    {
        harvestingMenu.Close();
        if (other.tag == "Player") Scannable = false;
    }


    private void Update()
    {
        if(scannable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                harvestingMenu.Open(tier1Element, tier2Element, tier3Element, rareElement, rareElementHarvestChance);
                harvestingMenu.CloseNote();
            }
        }
    }
}
