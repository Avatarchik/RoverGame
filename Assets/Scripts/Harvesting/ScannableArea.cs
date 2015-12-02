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

    private bool scannable = true;
    private Harvesting harvestingMenu;

    public bool Scannable
    {
        get { return scannable; }
        set
        {
            scannable = value;

            //also something about showing an element on the screen?
            harvestingMenu.Toggle();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") Scannable = true;
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") Scannable = false;
    }


    private void Update()
    {
        if(Scannable && Input.GetKeyDown(KeyCode.Space))
        {
            //attempting to scan area open scanning menu!
        }
    }
}
