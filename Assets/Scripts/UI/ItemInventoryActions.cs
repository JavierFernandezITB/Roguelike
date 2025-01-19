using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventoryActions : ServicesReferences
{
    public Button UseButton;
    public Button EquipButton;
    public Button DropButton;

    public void Awake()
    {
        ResetActions();
    }

    public void ResetActions()
    {
        // Ok, unity peta aquí y he intentado de todo pero no funciona, así que lo siento, pero el ibuprofeno no da para tanto. :D

        UseButton = transform.GetChild(0).GetComponent<Button>();
        EquipButton = transform.GetChild(1).GetComponent<Button>();
        DropButton = transform.GetChild(2).GetComponent<Button>();

        UseButton.gameObject.SetActive(false);
        UseButton.onClick.RemoveAllListeners();
        EquipButton.gameObject.SetActive(false);
        EquipButton.onClick.RemoveAllListeners();
        DropButton.gameObject.SetActive(false);
        DropButton.onClick.RemoveAllListeners();
    }

    public void SetInventoryActions(AItem item)
    {
        if (item.itemData.canBeUsed)
        {
            UseButton.gameObject.SetActive(true);
            UseButton.onClick.AddListener(() => { item.Use(); });
        }
        if (item.itemData.canBeEquipped)
        {
            EquipButton.gameObject.SetActive(true);
            EquipButton.onClick.AddListener(() => { item.Equip(); });
        }
        if (item.itemData.canBeDropped)
        {
            DropButton.gameObject.SetActive(true);
            DropButton.onClick.AddListener(() => { item.Drop(); });
        }
    }
}
