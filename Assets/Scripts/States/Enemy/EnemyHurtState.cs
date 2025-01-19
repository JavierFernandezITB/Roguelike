using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : AEnemyBaseState
{
    public Color hurtColor = Color.red;
    private Color originalColor;

    private AudioSource audioSource;

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Enemy Entered State: Hurt");
        originalColor = enemy.spriteRenderer.color;
        enemy.spriteRenderer.color = hurtColor;

        if (audioSource == null)
            audioSource = enemy.GetComponent<AudioSource>();
        if (enemy.hurtSound != null)
            audioSource.PlayOneShot(enemy.hurtSound);

        enemy.StartCoroutine(HurtDelay(enemy));

        if (enemy.enemyType != EEnemyType.Shooter)
            enemy.SwitchState(enemy.chasingState);
        else
            enemy.SwitchState(enemy.idleState);
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
