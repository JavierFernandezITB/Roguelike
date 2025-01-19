using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDescription : ServicesReferences
{
    public Image itemImage;
    public Text itemTitle;
    public Text itemDescription;

    public void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        try
        {
            itemImage.gameObject.SetActive(false);
        }
        catch
        {
            itemImage = null;
        }
        itemTitle.text = "";
        itemDescription.text = "";
    }

    public void SetDescription(AItem item)
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = item.itemData.sprite;
        itemTitle.text = item.itemData.itemName;
        itemDescription.text = item.itemData.description;
    }
}
