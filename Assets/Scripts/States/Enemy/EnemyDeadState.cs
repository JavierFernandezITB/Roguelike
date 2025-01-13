using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyDeadState : AEnemyBaseState
{
    InventoryManagerService inventoryManagerService;
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Enemy Entered State: Dead");
        inventoryManagerService = GameObject.Find("/InventoryManagerService").GetComponent<InventoryManagerService>();
        inventoryManagerService.coins += Random.Range(enemy.MinimumCoinReward, enemy.MaximumCoinReward);
        enemy.currentRoomHandler.enemiesRemaining -= 1;
        enemy.currentRoomHandler.spawnedEnemiesPool.Remove(enemy.gameObject);
        enemy.DestroyEntity();
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

    }
}
