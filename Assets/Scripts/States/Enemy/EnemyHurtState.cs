using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyHurtState : AEnemyBaseState
{
    public Color hurtColor = Color.red;
    private Color originalColor;

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Enemy Entered State: Hurt");
        originalColor = enemy.spriteRenderer.color;
        enemy.spriteRenderer.color = hurtColor;
        enemy.StartCoroutine(HurtDelay(enemy));
        enemy.SwitchState(enemy.chasingState);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

    }

    private IEnumerator HurtDelay(EnemyStateManager character)
    {
        yield return new WaitForSeconds(.25f);
        character.spriteRenderer.color = originalColor;
    }
}
