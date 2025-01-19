using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyDeadState : AEnemyBaseState
{
    InventoryManagerService inventoryManagerService;
    public List<ItemSO> droppableItems = new List<ItemSO>();
    public int dropChance = 20;
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Enemy Entered State: Dead");
        inventoryManagerService = GameObject.Find("/InventoryManagerService").GetComponent<InventoryManagerService>();
        inventoryManagerService.coins += Random.Range(enemy.MinimumCoinReward, enemy.MaximumCoinReward);

        // si el resultado está entre 0 y el dropchance (ej: 20) entonces hay drop random, si no, no.
        if (Random.Range(0, 100) < dropChance)
        {
            Debug.Log("DROP!");
            foreach (ItemSO item in inventoryManagerService.itemDatabase.currentActiveItems)
            {
                if (item.isInDropPool)
                {
                    Debug.Log(item.itemName);
                    droppableItems.Add(item);
                }
            }

            ItemSO chosenItem = droppableItems[Random.Range(0, droppableItems.Count)];

            GameObject drop = enemy.SpawnDrop(chosenItem.prefab);
            drop.transform.position = enemy.transform.position;
            drop.transform.localScale = new Vector3(chosenItem.itemScale, chosenItem.itemScale, chosenItem.itemScale);
        }

        enemy.currentRoomHandler.enemiesRemaining -= 1;
        enemy.currentRoomHandler.spawnedEnemiesPool.Remove(enemy.gameObject);
        enemy.DestroyEntity();
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

    }
}
