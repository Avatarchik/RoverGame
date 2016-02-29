using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class Inventory : Menu
    {
        public InventorySlot inventorySlotPrefab;
        public Transform InventorySlotContainer;

        public InventoryInfoPanel infoPanel;

        public ToggleGroup toggleGroup;

        public Button dropButton;
        public Button useButton;
        //public Button closeButton;

        public List<Ingredient> ingredientsInInventory = new List<Ingredient>();

        public bool ContainerExchange = false;

        private List<InventorySlot> inventorySlots = new List<InventorySlot>();



        public float Weight
        {
            get
            {
                float weight = 0.01f;

                foreach (Ingredient i in ingredientsInInventory)
                {
                    weight += i.weight;
                }

                return weight;
            }
        }



        public override void Open()
        {
            base.Open();
        }


        public void Open(bool containerExchange = false)
        {
            if(!IsActive)
            {
                if (containerExchange)
                {
                    Debug.Log("exchange desired");
                    CloseInfoPanel();
                    base.Open();
                }
                else
                {
                    Debug.Log("no exchange");
                    OpenInfoPanel();
                    base.Open();
                }

                ContainerExchange = containerExchange;
            }
        }


        public override void Close()
        {
            Container container = UIManager.GetMenu<Container>();
            if (container.IsActive) container.Close();
            base.Close();
        }


        public virtual void OpenInfoPanel()
        {
            Debug.Log("initializing info panel!");
            if (ingredientsInInventory.Count > 0)
            {
                Debug.Log("more than one ingredient : opening info panel!");
                infoPanel.Initialize(ingredientsInInventory[0], GetIngredientAmount(ingredientsInInventory[0]));
                infoPanel.gameObject.SetActive(true);
            }
        }


        public virtual void CloseInfoPanel()
        {
            infoPanel.gameObject.SetActive(false);
        }


        public virtual int GetIngredientAmount(int ingredientId)
        {
            int count = 0;
            foreach (Ingredient i in ingredientsInInventory)
            {
                if (i.id == ingredientId) count++;
            }
            return count;
        }


        public virtual int GetIngredientAmount(Ingredient ingredient)
        {
            int count = 0;
            foreach (Ingredient i in ingredientsInInventory)
            {
                if (i.id == ingredient.id) count++;
            }
            return count;
        }


        public virtual void InitializeInventorySlots()
        {
            for (int i = inventorySlots.Count - 1; i >= 0; i--)
            {
                Destroy(inventorySlots[i].gameObject);
            }

            inventorySlots.Clear();
            List<Ingredient> encounteredIngredients = new List<Ingredient>();
            foreach (Ingredient i in ingredientsInInventory)
            {
                if (!encounteredIngredients.Contains(i))
                {
                    BuildInventorySlot(i, GetIngredientAmount(i));
                    encounteredIngredients.Add(i);
                }
            }
        }


        public virtual void BuildInventorySlot(Ingredient ingredient, int count)
        {
            InventorySlot newSlot = Instantiate(inventorySlotPrefab) as InventorySlot;
            newSlot.transform.SetParent(InventorySlotContainer);
            newSlot.transform.localScale = Vector3.one;
            newSlot.moreInfo.group = toggleGroup;

            if(newSlot.image != null) newSlot.image.sprite = ingredient.image;
            newSlot.Amount = count;
            newSlot.SlotIngredient = ingredient;
            newSlot.Amount = count;

            inventorySlots.Add(newSlot);
            newSlot.gameObject.SetActive(true);
        }


        public virtual void AddInventoryItem(Ingredient ingredient, int count)
        {
            if(ingredient != null)
            {
                for (int i = count; i > 0; i--)
                {
                    ingredientsInInventory.Add(ingredient);
                }

                MessageMenu mm = UIManager.GetMenu<MessageMenu>();
                if (count == 1)
                {
                    mm.Open(string.Format("{0} added", ingredient.displayName), 3);
                }
                else
                {
                    mm.Open(string.Format("{0} {1}s added", count, ingredient.displayName), 3);
                }

                InitializeInventorySlots();
            }
        }


        public virtual void RemoveInventoryItem(Ingredient ingredient, int count)
        {
            bool foundNothing = true;
            while (count > 0)
            {
                for (int i = 0; i < ingredientsInInventory.Count; i++)
                {
                    if (ingredientsInInventory[i].id == ingredient.id)
                    {
                        ingredientsInInventory.RemoveAt(i);
                        count--;
                        foundNothing = false;
                        break;
                    }
                }

                if (foundNothing)
                {
                    Debug.LogError("No item : " + ingredient.displayName + " : id : " + ingredient.id + " was found");
                    break;
                }
            }

            MessageMenu mm = UIManager.GetMenu<MessageMenu>();
            if (count == 1)
            {
                mm.Open(string.Format("{0} removed", ingredient.displayName), 3);
            }
            else
            {
                mm.Open(string.Format("{0} {1}s removed", count, ingredient.displayName), 3);
            }

            InitializeInventorySlots();
        }


        public virtual void RemoveAllInventoryItems()
        {
            ingredientsInInventory.Clear();
            InitializeInventorySlots();
        }


        private void Transfer()
        {
            Close();
            UIManager.Open<Container>();
        }


        private void Awake()
        {
            //closeButton.onClick.AddListener(Close);
            InitializeInventorySlots();
        }
    }
}

