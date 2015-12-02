﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScannableTier : MonoBehaviour
{
    public enum Tier { first, second, third }

    public Tier myTier = Tier.first;

    public Button scanButton;
    public Button harvestButton;
    public Text title;
    public Text description;
    public Image icon;
    public Image fillIcon;

    public GameObject scannerUIRoot;
    public GameObject harvesterUIRoot;

    public InventoryIngredient ingredient;

    private Inventory playerInventory;

    public void Harvest()
    {
        playerInventory.AddInventoryItem(ingredient);
    }


    public void Scan()
    {
        StartCoroutine(ScanCoroutine());
    }


    public void Initialize(InventoryIngredient i)
    {
        ingredient = i;
        scannerUIRoot.SetActive(true);
        harvesterUIRoot.SetActive(false);

    }


    private IEnumerator ScanCoroutine()
    {
        float desiredTime = 1f;

        switch(myTier)
        {
            case Tier.first :
                desiredTime = 3f;
                break;

            case Tier.second:
                desiredTime = 5f;
                break;

            case Tier.third:
                desiredTime = 8f;
                break;

            default:
                break;
        }

        float elapsedTime = 0f;

        while(elapsedTime < desiredTime)
        {
            fillIcon.fillAmount = 1 - (elapsedTime / desiredTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        title.text = ingredient.ingredient.displayName;
        description.text = ingredient.ingredient.description;
        icon.sprite = ingredient.ingredient.image;

        scannerUIRoot.SetActive(false);
        harvesterUIRoot.SetActive(true);
    }


    private void Awake()
    {
        harvestButton.onClick.AddListener(Harvest);
        scanButton.onClick.AddListener(Scan);

        if (playerInventory == null) playerInventory = GameObject.FindObjectOfType<Inventory>() as Inventory;
    }
}
