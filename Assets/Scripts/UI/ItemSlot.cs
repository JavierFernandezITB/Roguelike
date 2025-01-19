using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : ServicesReferences
{
    public Image itemImageSlotObject;
    public Text itemQuantityText;

    public AItem itemReference;

    public event Action<ItemSlot> OnItemClicked, OnItemDropped, OnItemBeginDragging, OnItemEndDragging;

    public bool isSlotEmpty = true;

    public void Awake()
    {
        ResetSlot();
    }

    public void ResetSlot()
    {
        itemImageSlotObject = transform.GetChild(0).GetComponent<Image>();
        this.itemImageSlotObject.gameObject.SetActive(false);
        isSlotEmpty = true;
    }

    public void SetData(InventoryItem item)
    {
        if (item.isEmpty)
            return;
        itemImageSlotObject.gameObject.SetActive(true);
        itemImageSlotObject.sprite = item.itemData.sprite;
        itemQuantityText.text = item.quantity.ToString();
        itemReference = item.itemData.prefab.GetComponent<AItem>();
        isSlotEmpty = false;
    }

    public void OnDrop()
    {
        OnItemDropped?.Invoke(this);
    }

    public void OnEndDrag()
    {
        OnItemEndDragging?.Invoke(this);
    }

    public void OnBeginDrag()
    {
        if (isSlotEmpty)
            return;
        OnItemBeginDragging?.Invoke(this);
    }

    public void OnPointerClick(BaseEventData data)
    {
        if (isSlotEmpty)
            return;
        PointerEventData pointerData = data as PointerEventData;
        if (pointerData.button == PointerEventData.InputButton.Left)
            OnItemClicked?.Invoke(this);
    }
}
