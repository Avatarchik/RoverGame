using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class Container : Menu
    {
        public ContainerSlot containerSlotPrefab;
        public Transform InventorySlotContainer;
        public Text title;

        public Button inventoryButton;
        public Button closeButton;

        public List<Ingredient> ingredientsInInventory = new List<Ingredient>();
        public List<ContainerSlot> containerSlots = new List<ContainerSlot>();

        private ContainerObject currentContainer;


        public override void Open()
        {
            base.Open();
        }


        public void Open(List<Ingredient> ingredients, ContainerObject container)
        {
            title.text = container.objectName;

            currentContainer = container;
            ingredientsInInventory = ingredients;
            InitializeInventorySlots();
            
            Open();
        }


        public override void Close()
        {
            UIManager.Close<Inventory>();
            UIManager.Close<InGameMainMenu>();
            base.Close();
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


        public virtual void AddInventoryItem(Ingredient ingredient, int count)
        {
            for (int i = count; i > 0; i--)
            {
                ingredientsInInventory.Add(ingredient);
            }
            Debug.Log("item added");
            InitializeInventorySlots();
        }


        public virtual void RemoveInventoryItem(Ingredient ingredient, int count)
        {
            if(ingredient != null)
            {
                if (ingredientsInInventory.Count < count)
                {
                    Debug.LogError("unable to comply, insufficient inventory ingredients");
                    return;
                }

                while (count > 0)
                {
                    for (int i = 0; i < ingredientsInInventory.Count; i++)
                    {
                        if (ingredientsInInventory[i].id == ingredient.id)
                        {
                            ingredientsInInventory.RemoveAt(i);
                            count--;
                            break;
                        }
                    }
                }

                InitializeInventorySlots();
            }
        }


        public virtual void InitializeInventorySlots()
        {
            for (int i = containerSlots.Count - 1; i >= 0; i--)
            {
                Destroy(containerSlots[i].gameObject);
            }

            containerSlots.Clear();
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
            if(ingredient!= null && count > 0)
            {
                if (!IsActive) Open();
                ContainerSlot newSlot = Instantiate(containerSlotPrefab) as ContainerSlot;
                newSlot.transform.SetParent(InventorySlotContainer);
                newSlot.transform.localScale = Vector3.one;

                if (newSlot.image != null) newSlot.image.sprite = ingredient.image;
                Debug.Log(ingredient.displayName);
                newSlot.SlotIngredient = ingredient;
                newSlot.Amount = count;

                containerSlots.Add(newSlot);
            }
        }


        private void OpenInventory()
        {
            Inventory inventory = UIManager.GetMenu<Inventory>();
            InGameMainMenu igmm = UIManager.GetMenu<InGameMainMenu>();

            inventory.Open(true);
            igmm.OpenInventoryTransfer(true, false);
        }


        private void Update()
        {
            if (IsActive)
            {
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape)) Close();
            }
        }


        private void Awake()
        {
            InitializeInventorySlots();

            closeButton.onClick.AddListener(Close);
            inventoryButton.onClick.AddListener(OpenInventory);
        }
    }
}