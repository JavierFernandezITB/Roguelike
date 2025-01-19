using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : AEnemyBaseState
{
    public int timeBeforeChasing = 3; // Seconds.
    public bool alreadyWaiting = false;

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Enemy Entered State: Idle");
        if (!alreadyWaiting)
            enemy.StartCoroutine(ChasingDelay(enemy));
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

    }

    private IEnumerator ChasingDelay(EnemyStateManager enemy)
    {
        alreadyWaiting = true;
        Debug.Log($"Waiting {timeBeforeChasing}s before chasing.");
        yield return new WaitForSeconds(timeBeforeChasing);
        alreadyWaiting = false;
        enemy.SwitchState(enemy.chasingState);
    }
}
