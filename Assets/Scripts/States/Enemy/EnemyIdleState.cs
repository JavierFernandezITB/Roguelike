using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : AEnemyBaseState
{
    public int timeBeforeChasing = 5; // Seconds.

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Enemy Entered State: Idle");
        enemy.StartCoroutine(ChasingDelay(enemy));
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

    }

    private IEnumerator ChasingDelay(EnemyStateManager enemy)
    {
        Debug.Log($"Waiting {timeBeforeChasing}s before chasing.");
        yield return new WaitForSeconds(timeBeforeChasing);
        enemy.SwitchState(enemy.chasingState);
    }
}
