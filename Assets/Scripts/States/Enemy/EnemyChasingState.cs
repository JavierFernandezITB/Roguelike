using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyChasingState : AEnemyBaseState
{

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Enemy Entered State: Chasing");
        enemy.target = GameObject.Find("/Character");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (enemy.target != null)
        {
            enemy.animator.SetBool("Walking", true);

            // Calculate the direction to the player
            Vector3 direction = (enemy.target.transform.position - enemy.transform.position).normalized;

            float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.target.transform.position);

            // Check if the enemy should move
            if (distanceToPlayer <= 1f)
            {
                enemy.animator.SetBool("Walking", false);
                enemy.SwitchState(enemy.attackingState);
            }

            // Move the enemy towards the player
            enemy.transform.position += direction * enemy.Speed * Time.deltaTime * 0.5f;

            if (direction.x > 0)
            {
                enemy.transform.localScale = new Vector3(1, 1, 1); // Facing right
            }
            else if (direction.x < 0)
            {
                enemy.transform.localScale = new Vector3(-1, 1, 1); // Facing left
            }
        }
    }
}
