using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopSlot : ServicesReferences
{
    public ItemSO currentItemInSlot;

    public InventoryManagerService inventoryManager;

    public event Action<ShopSlot> OnItemClicked;


    public void OnPointerClick(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
        if (pointerData.button == PointerEventData.InputButton.Left)
            OnItemClicked?.Invoke(this);
    }
}
