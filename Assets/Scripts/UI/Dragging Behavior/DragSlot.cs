using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

namespace Sol
{
    public class DragSlot : MonoBehaviour, IDropHandler
    {
        #region IDropHandler implementation
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log(1);
            InventorySlot inventorySlot = DragHandler.itemBeingDragged.GetComponent<InventorySlot>();
            if (inventorySlot != null)
            {
                Debug.Log(2);
                if(inventorySlot.SlotIngredient as EquipableItem) inventorySlot.ForceEquip();
            }
        }
        #endregion
    }
}