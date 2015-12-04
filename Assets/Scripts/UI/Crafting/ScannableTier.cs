using UnityEngine;
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
    public Image mainicon;
    public Image fillIcon;

    public GameObject scannerUIRoot;
    public GameObject harvesterUIRoot;

    public InventoryIngredient ingredient;
    public InventoryIngredient rareIngredient;

    private int variance = 1; 
    private float rareDropChance = 0.15f;

    private Inventory playerInventory;

    public int Variance
    {
        get { return variance; }
        set
        {
            variance = value;
        }
    }

    public float RareDropChance
    {
        get { return rareDropChance; }
        set
        {
            rareDropChance = value;
        }
    }

    public void Harvest()
    {
        StartCoroutine(HarvestCoroutine());
    }


    public void Scan()
    {
        StartCoroutine(ScanCoroutine());
    }


    public void Initialize(InventoryIngredient i, InventoryIngredient ri, float rdc)
    {
        ingredient = i;
        RareDropChance = rdc;
        rareIngredient = ri;
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
        mainicon.sprite = ingredient.ingredient.image;

        scannerUIRoot.SetActive(false);
        harvesterUIRoot.SetActive(true);
    }


    public IEnumerator HarvestCoroutine()
    {
        harvestButton.enabled = false;
        float desiredTime = 1f;

        switch (myTier)
        {
            case Tier.first:
                desiredTime = 5f;
                break;

            case Tier.second:
                desiredTime = 10f;
                break;

            case Tier.third:
                desiredTime = 15f;
                break;

            default:
                break;
        }

        float elapsedTime = 0f;

        while (elapsedTime < desiredTime)
        {
            mainicon.fillAmount = 1 - (elapsedTime / desiredTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainicon.fillAmount = 1;

        //add varianced ingredient amount
        int val = ingredient.amount;
        ingredient.amount = Mathf.RoundToInt(Random.Range(val - variance, val + variance));
        playerInventory.AddInventoryItem(ingredient);
        ingredient.amount = val;

        //possibly add a rare element drop
        if(Random.Range(0f, 1f) < RareDropChance)
        {
            //actually add the rare drop, they got lucky.
            playerInventory.AddInventoryItem(rareIngredient);
        }

        playerInventory.Open();

        harvestButton.enabled = true;
    }


    private void Awake()
    {
        harvestButton.onClick.AddListener(Harvest);
        scanButton.onClick.AddListener(Scan);

        if (playerInventory == null) playerInventory = GameObject.FindObjectOfType<Inventory>() as Inventory;
    }
}
