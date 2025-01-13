using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyAttackingState : AEnemyBaseState
{
    public int timeBeforeChasing = 5; // Seconds.

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Enemy Entered State: Attack");

        if (enemy.enemyType == EEnemyType.Melee)
        {
            enemy.target.GetComponent<CharacterStateManager>().DealDamage(enemy.Damage);
        } else if (enemy.enemyType == EEnemyType.Bomber)
        {
            enemy.target.GetComponent<CharacterStateManager>().DealDamage(enemy.Damage);
            enemy.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            enemy.StartCoroutine(DelayBomberDestruction(enemy));
            
        }
        enemy.SwitchState(enemy.idleState);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

    }

    public IEnumerator DelayBomberDestruction(EnemyStateManager enemy)
    {
        enemy.spriteRenderer.enabled = false;
        enemy.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        enemy.transform.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1);
        enemy.SwitchState(enemy.deadState);
    }
}
