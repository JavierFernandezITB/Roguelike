using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AItem : ServicesReferences
{
    public ItemSO itemData;
    public int quantity;

    private bool isPlayerInRange = false;
    private CharacterControls controls;

    private Action<InputAction.CallbackContext> onPickupAction;

    public abstract void Use();

    public abstract void Equip();

    public abstract void Passive();

    public void Drop()
    {
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
        inventoryManagerService.AddItemToInventory(this.gameObject);
    }
}
