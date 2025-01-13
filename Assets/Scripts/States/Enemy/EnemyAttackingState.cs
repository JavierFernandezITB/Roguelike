using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : AEnemyBaseState
{
    public int timeBeforeChasing = 5; // Seconds.

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Enemy Entered State: Attack");

        if (enemy.enemyType == EEnemyType.Melee)
        {
            enemy.target.GetComponent<CharacterStateManager>().DealDamage(enemy.Damage);
        }

        enemy.SwitchState(enemy.idleState);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

    }
}
