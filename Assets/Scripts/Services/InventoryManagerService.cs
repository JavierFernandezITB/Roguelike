using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class InventoryManagerService : ServicesReferences
{
    public ItemSlot inventorySlotPrefab;

    public RectTransform inventoryRectTransform;

    public InventoryDescription inventoryDescription;

    public ItemInventoryActions inventoryActions;

    public GameObject inventoryBackground;

    public int inventorySize;

    List<ItemSlot> inventorySlots = new List<ItemSlot>();

    public List<InventoryItem> inventoryItems = new List<InventoryItem>();

    public AItem testItem;

    private bool areActionsSet = false;

    // cosas de monedas y eso xd
    public Text uiCoinsDisplay;
    public int coins = 0;

    // reutilizo los controles del personaje por que xd
    private CharacterControls controls;

    public void Awake()
    {
        base.Persist<InventoryManagerService>();

        HideInventoryUI();

        controls = new CharacterControls();
        controls.Enable();

        controls.Character.ToggleInventory.performed += _ => DisplayInventoryUI();

        InstantiateInventory();
        LoadInventoryItems();

        //test
        AddItemToInventory(testItem, false);
    }

    public void Update()
    {
        uiCoinsDisplay.text = $"Coin Purse: {coins}";
    }

    public bool SubstractCoins(int amount)
    {
        Debug.Log("Trying to substract coins.");
        if (coins - amount >= 0)
        {
            coins -= amount;
            return true;
        }
        Debug.Log("Not enough coins in the player's purse.");
        return false;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    public void InstantiateInventory()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            ItemSlot slot = Instantiate(inventorySlotPrefab);
            slot.transform.SetParent(inventoryRectTransform);
            slot.transform.localScale = new Vector3(1, 1, 1);
            slot.name = $"ItemSlot ({i})";
            inventorySlots.Add(slot);
            slot.OnItemClicked += HandleItemClick;
            slot.OnItemBeginDragging += HandleBeginDragging;
            slot.OnItemEndDragging += HandleEndDragging;
            slot.OnItemDropped += HandleSwap;
        }
    }

    public void LoadInventoryItems()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            inventorySlots[i].ResetSlot();
        }
            
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            inventorySlots[i].SetData(inventoryItems[i]);
        }
    }

    public void AddItemToInventory(AItem newItem, bool shouldDestroy = true)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            InventoryItem currentItem = inventoryItems[i];
            if (currentItem.itemData.itemName == newItem.itemData.itemName)
            {
                if (currentItem.quantity < currentItem.itemData.maxQuantity)
                {
                    Debug.Log("Changed quantity");
                    Debug.Log(currentItem.quantity);
                    inventoryItems[i] = currentItem.ChangeQuantity(currentItem.quantity + 1);
                    Destroy(newItem.gameObject);
                    LoadInventoryItems();
                    Debug.Log("Item stacked successfully.");
                    return;
                }
            }
        }

        if (inventoryItems.Count >= inventorySize)
        {
            Debug.Log("Inventory is full. Cannot add new item.");
            return;
        }

        InventoryItem newInvItem = InventoryItem.GetEmptyItem();
        newInvItem.itemData = newItem.itemData;
        newInvItem.quantity = 1;
        inventoryItems.Add(newInvItem);
        if (shouldDestroy)
            Destroy(newItem.gameObject);
        LoadInventoryItems();
        Debug.Log("Item added to inventory.");
    }


    public void DeleteItemFromInventory(AItem item)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            InventoryItem currentItem = inventoryItems[i];
            if (currentItem.itemData.itemName == item.itemData.itemName)
            {
                if (currentItem.quantity > 1)
                {
                    inventoryItems[i] = currentItem.ChangeQuantity(currentItem.quantity - 1);
                    LoadInventoryItems();
                    inventoryDescription.ResetDescription();
                    inventoryActions.ResetActions();
                    return;
                } else
                {
                    inventoryItems.Remove(currentItem);
                    LoadInventoryItems();
                    inventoryDescription.ResetDescription();
                    inventoryActions.ResetActions();
                    return;
                }

            }
        }
    }

    public void DropItemFromInventory(AItem item)
    {
        GameObject droppedItem;
        GameObject character = GameObject.Find("/Character");
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            InventoryItem currentItem = inventoryItems[i];
            if (currentItem.itemData.itemName == item.itemData.itemName)
            {
                if (currentItem.quantity > 1)
                {
                    inventoryItems[i] = currentItem.ChangeQuantity(currentItem.quantity - 1);
                    droppedItem = Instantiate((item.itemData.prefab) as GameObject);
                    droppedItem.transform.position = new Vector2(character.transform.position.x, character.transform.position.y);
                    droppedItem.transform.localScale = new Vector3(currentItem.itemData.itemScale, currentItem.itemData.itemScale, currentItem.itemData.itemScale);
                    LoadInventoryItems();
                    return;
                } else
                {
                    inventoryItems.Remove(currentItem);
                    droppedItem = Instantiate((item.itemData.prefab) as GameObject);
                    droppedItem.transform.position = new Vector2(character.transform.position.x, character.transform.position.y);
                    droppedItem.transform.localScale = new Vector3(currentItem.itemData.itemScale, currentItem.itemData.itemScale, currentItem.itemData.itemScale);
                    LoadInventoryItems();
                    inventoryDescription.ResetDescription();
                    inventoryActions.ResetActions();
                    return;
                }

            }
        }
    }

    private void HandleSwap(ItemSlot obj)
    {

    }

    private void HandleEndDragging(ItemSlot obj)
    {

    }

    private void HandleBeginDragging(ItemSlot obj)
    {

    }

    private void HandleItemClick(ItemSlot obj)
    {
        if (!areActionsSet)
        {
            areActionsSet = true;
            inventoryDescription.SetDescription(obj.itemReference);
            inventoryActions.SetInventoryActions(obj.itemReference);
        }
    }

    public void DisplayInventoryUI()
    {
        Debug.Log("inventory on / off");
        // obsoleto pero la alternativa no rula lol
        if (inventoryBackground.activeSelf)
            HideInventoryUI();
        else
            ShowInventoryUI();
    }

    public void ShowInventoryUI()
    {
        inventoryBackground.gameObject.SetActive(true);
        LoadInventoryItems();
        inventoryDescription.ResetDescription();
        inventoryActions.ResetActions();
        areActionsSet = false;
    }

    public void HideInventoryUI()
    {
        inventoryBackground.gameObject.SetActive(false);
        inventoryDescription.ResetDescription();
        inventoryActions.ResetActions();
        areActionsSet = false;
    }
}

public struct InventoryItem
{
    public int quantity;
    public ItemSO itemData;

    public bool isEmpty => itemData == null;

    public InventoryItem ChangeQuantity(int newQuantity)
    {
        return new InventoryItem()
        {
            itemData = this.itemData,
            quantity = newQuantity,
        };
    }

    public static InventoryItem GetEmptyItem()
    {
        return new InventoryItem()
        {
            itemData = null,
            quantity = 0,
        };
    }
}