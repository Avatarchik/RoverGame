using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sol
{
    public class ScannableTier : MonoBehaviour
    {
        public enum Tier { first, second, third }

        public Tier myTier = Tier.first;

        public Button scanButton;
        public Button harvestButton;
        public Text title;
        public Text description;
        public Image fillIcon2;
        public Image fillIcon1;
        public Image mainIcon;

        public GameObject scannerUIRoot;
        public GameObject harvesterUIRoot;

        public InventoryIngredient ingredient;
        public InventoryIngredient rareIngredient;

        private int variance = 1;
        private float rareDropChance = 0.15f;

        private Player player;

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
            fillIcon1.fillAmount = 1f;
            fillIcon2.fillAmount = 1f;
            ingredient = i;
            RareDropChance = rdc;
            rareIngredient = ri;
            harvestButton.enabled = true;
            scanButton.enabled = true;
            scannerUIRoot.SetActive(true);
            harvesterUIRoot.SetActive(false);
        }


        public void Halt()
        {
            StopAllCoroutines();
        }


        private IEnumerator ScanCoroutine()
        {
            float desiredTime = 1f;

            switch (myTier)
            {
                case Tier.first:
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

            desiredTime = Mathf.Clamp(desiredTime - player.Stats.ScanningSpeed, 0f, 10f);
            float elapsedTime = 0f;

            while (elapsedTime < desiredTime)
            {
                fillIcon1.fillAmount = 1 - (elapsedTime / desiredTime);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            title.text = ingredient.ingredient.displayName;
            description.text = ingredient.ingredient.description;
            mainIcon.sprite = ingredient.ingredient.image;

            scannerUIRoot.SetActive(false);
            harvesterUIRoot.SetActive(true);
        }


        public IEnumerator HarvestCoroutine()
        {
            harvestButton.enabled = false;
            Inventory inventory = UIManager.GetMenu<Inventory>();
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

            desiredTime = Mathf.Clamp(desiredTime - player.Stats.HarvestSpeed, 0f, 10f);
            float elapsedTime = 0f;

            while (elapsedTime < desiredTime)
            {
                fillIcon2.fillAmount = 1 - (elapsedTime / desiredTime);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            fillIcon2.fillAmount = 1;

            int val = ingredient.amount;
            ingredient.amount = Mathf.RoundToInt(Random.Range(val - variance, val + variance));
            inventory.AddInventoryItem(ingredient.ingredient, ingredient.amount);
            ingredient.amount = val;

            if (Random.Range(0f, 1f) < RareDropChance)
            {
                inventory.AddInventoryItem(rareIngredient.ingredient, rareIngredient.amount);
            }

            if (!inventory.IsActive) inventory.Open();

            harvestButton.enabled = true;
        }


        private void Awake()
        {
            harvestButton.onClick.AddListener(Harvest);
            scanButton.onClick.AddListener(Scan);
        }
    }

}
