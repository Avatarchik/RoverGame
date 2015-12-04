using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Harvesting : Menu
{
    public ScannableTier tier1;
    public ScannableTier tier2;
    public ScannableTier tier3;

    public GameObject screenNotificationRoot;


    public override void Open()
    {

        base.Open();
    }


    public void Open(InventoryIngredient i1, InventoryIngredient i2, InventoryIngredient i3, InventoryIngredient rareIngredient, float rareDropChance)
    {
        tier1.Initialize(i1, rareIngredient, rareDropChance);
        tier2.Initialize(i2, rareIngredient, rareDropChance);
        tier3.Initialize(i3, rareIngredient, rareDropChance);
        Open();
    }


    public void OpenNote()
    {
        screenNotificationRoot.SetActive(true);
    }


    public void CloseNote()
    {
        screenNotificationRoot.SetActive(false);
    }


    public override void Close()
    {
        base.Close();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Close();
        }
    }
}
