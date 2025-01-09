using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public List<AItem> inventoryItems = new List<AItem>();

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

        //test
        //inventoryItems.Add(testItem);

        HideInventoryUI();

        controls = new CharacterControls();
        controls.Enable();

        controls.Character.ToggleInventory.performed += _ => DisplayInventoryUI();

        InstantiateInventory();
        LoadInventoryItems();
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

    public void AddItemToInventory(GameObject obj)
    {
        bool done = false;

        AItem item = obj.GetComponent<AItem>();


        for (int i = 0; i < inventoryItems.Count; i++)
        {
            AItem currentItem = inventoryItems[i];
            if (currentItem.itemData.itemName == item.itemData.itemName)
            {
                if (currentItem.quantity < currentItem.itemData.maxQuantity)
                {
                    Debug.Log("kekw");
                    currentItem.quantity += 1;
                    Destroy(item.gameObject);
                    LoadInventoryItems();
                    done = true;
                    break;
                }
            }
        }

        if (inventoryItems.Count >= inventorySize)
        {
            Debug.Log("Inventory full.");
            return;
        }

        if (done)
            return;

        inventoryItems.Add(item);
        Destroy(item.gameObject);
        LoadInventoryItems();
    }

    public void DropItemFromInventory(AItem item)
    {
        GameObject droppedItem;
        GameObject character = GameObject.Find("/Character");
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            AItem currentItem = inventoryItems[i];
            if (currentItem.itemData.itemName == item.itemData.itemName)
            {
                if (currentItem.quantity > 1)
                {
                    currentItem.quantity -= 1;
                    droppedItem = Instantiate((item.itemData.prefab) as GameObject);
                    droppedItem.transform.position = new Vector2(character.transform.position.x, character.transform.position.y);
                    LoadInventoryItems();
                    return;
                }
            }
        }

        Debug.Log("abupas");
        inventoryItems.Remove(item);
        Debug.Log(inventoryItems.Count);
        droppedItem = Instantiate((item.itemData.prefab) as GameObject);
        droppedItem.transform.position = new Vector2(character.transform.position.x, character.transform.position.y);
        LoadInventoryItems();
        inventoryDescription.ResetDescription();
        inventoryActions.ResetActions();
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
