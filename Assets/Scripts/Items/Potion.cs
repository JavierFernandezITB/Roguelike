using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : AItem
{
    public int health = 25;
    public AudioClip clip;
    public AudioSource source;

    public override void Use()
    {
        source = GameObject.Find("/THIRD SOUND OK").GetComponent<AudioSource>();
        source.PlayOneShot(clip);
        inventoryManagerService = GameObject.Find("/InventoryManagerService").GetComponent<InventoryManagerService>();
        CharacterStateManager csm = GameObject.Find("/Character").GetComponent<CharacterStateManager>();
        if (csm.Health + health > csm.MaxHealth)
            csm.Health = csm.MaxHealth;
        else
            csm.Health += health;
        inventoryManagerService.DeleteItemFromInventory(this);
    }

    public override void Attack()
    {

    }

    public override void Passive()
    {

    }

}
