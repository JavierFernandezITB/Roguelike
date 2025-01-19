using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : ServicesReferences
{
    public GameObject shopUiComponent;

    public ItemSO[] currentItemsInShop;
    public ItemsDatabaseSO itemsDatabase;
    public List<ItemSO> itemsThatCanBeSold;
    public int currentListIndex = 0;

    public int itemRotationCooldownMinutes = 5;

    public void Awake()
    {
        currentItemsInShop = new ItemSO[3];
        itemsThatCanBeSold = new List<ItemSO>();
        Debug.Log("Shop changed items.");
        for (int i = 0; i < itemsDatabase.currentActiveItems.Count; i++)
        {
            if (itemsDatabase.currentActiveItems[i].canBeBought)
                itemsThatCanBeSold.Add(itemsDatabase.currentActiveItems[i]);
        }

        RotateStore();
    }

    public void RotateStore()
    {
        for (int i = 0; i < 3; i++)
        {
            int randnum = Random.Range(0, itemsThatCanBeSold.Count);
            Debug.Log(randnum);
            currentItemsInShop[i] = itemsThatCanBeSold[randnum];
        }
        for (int i = 0; i < 3; i++)
        {

            shopUiComponent.transform.GetChild(i).GetComponent<ShopSlot>().OnItemClicked += BuyItem;
            shopUiComponent.transform.GetChild(i).GetComponent<ShopSlot>().currentItemInSlot = currentItemsInShop[i];
            shopUiComponent.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>().text = $"{currentItemsInShop[i].price} Coins";
            shopUiComponent.transform.GetChild(i).transform.GetChild(1).GetComponent<Image>().sprite = currentItemsInShop[i].sprite;
        }

    }

    public void BuyItem(ShopSlot obj)
    {
        InventoryManagerService inventoryManagerService = GameObject.Find("/InventoryManagerService").GetComponent<InventoryManagerService>();
        if (inventoryManagerService.SubstractCoins(obj.currentItemInSlot.price))
        {
            GameObject newItem = Instantiate(obj.currentItemInSlot.prefab);
            newItem.transform.position = new Vector2(GameObject.Find("/Character").transform.position.x, GameObject.Find("/Character").transform.position.y);
            inventoryManagerService.AddItemToInventory(newItem.GetComponent<AItem>());
            Debug.Log($"Player bought {obj.currentItemInSlot.itemName} for {obj.currentItemInSlot.price}");
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (shopUiComponent == null)
                shopUiComponent = GameObject.Find("/Main Camera/MainUI/Panel/Shop");
            shopUiComponent.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (shopUiComponent == null)
                shopUiComponent = GameObject.Find("/Main Camera/MainUI/Panel/Shop");
            shopUiComponent.SetActive(false);
        }
    }
}
