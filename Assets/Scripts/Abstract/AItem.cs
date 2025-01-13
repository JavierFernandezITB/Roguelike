using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AItem : ServicesReferences
{
    public ItemSO itemData;

    private bool isPlayerInRange = false;
    public bool isEquipped = false;
    private CharacterControls controls;

    private Action<InputAction.CallbackContext> onPickupAction;

    public abstract void Use();

    public abstract void Attack();

    public void Equip()
    {
        GameObject character = GameObject.Find("/Character");
        GameObject weaponParent = GameObject.Find("/Character/WeaponParent");
        try 
        {
            Destroy(weaponParent.transform.GetChild(0).gameObject);
        }
        catch (Exception e) { }
        GameObject newPhysicalItem = Instantiate(itemData.prefab);
        newPhysicalItem.transform.localScale = new Vector3(itemData.itemScale, itemData.itemScale, itemData.itemScale);
        newPhysicalItem.transform.parent = weaponParent.transform;
        newPhysicalItem.transform.position = new Vector3(character.transform.position.x + 0.575f, character.transform.position.y, character.transform.position.z);
        weaponParent.GetComponent<WeaponParent>().currentItem = newPhysicalItem.GetComponent<AItem>();
        newPhysicalItem.GetComponent<AItem>().isEquipped = true;
    }

    public abstract void Passive();

    public void Drop()
    {
        isEquipped = false;
        InventoryManagerService inventoryManagerService = GameObject.Find("InventoryManagerService").GetComponent<InventoryManagerService>();
        inventoryManagerService.DropItemFromInventory(this);
    }

    private void Awake()
    {
        // just in case its needed in any item.
        base.GetServices();

        controls = new CharacterControls();
        controls.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            onPickupAction = ctx => OnPickup();
            controls.Character.Pickup.performed += onPickupAction;
            this.isPlayerInRange = true;
            DisplayPickupPrompt();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controls.Character.Pickup.performed -= onPickupAction;
            this.isPlayerInRange = false;
            HidePickupPrompt();
        }
    }

    private void DisplayPickupPrompt()
    {
        Debug.Log("Press E to pick up");
    }

    private void HidePickupPrompt()
    {
        Debug.Log("No longer in range of item.");
    }

    public void OnPickup() 
    {
        controls.Character.Pickup.performed -= onPickupAction;
        if (!isEquipped)
            inventoryManagerService.AddItemToInventory(this);
    }
}
