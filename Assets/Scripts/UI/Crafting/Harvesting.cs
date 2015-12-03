using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Harvesting : Menu
{
    public ScannableTier tier1;
    public ScannableTier tier2;
    public ScannableTier tier3;


    public override void Open()
    {

        base.Open();
    }


    public void Open(InventoryIngredient i1, InventoryIngredient i2, InventoryIngredient i3)
    {
        tier1.Initialize(i1);
        tier2.Initialize(i2);
        tier3.Initialize(i3);
        Open();
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
