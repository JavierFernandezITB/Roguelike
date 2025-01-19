using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : AItem
{
    public bool attackBlocked = false;
    public float attackDelay = 1f;
    public float damagePerSwing = 25f;
    public Animator animator;


    public override void Use()
    {
    }

    public override void Attack()
    {
        if (attackBlocked)
            return;
        animator.SetTrigger("Attack");
        foreach (EnemyStateManager enemy in transform.GetChild(0).GetComponent<SwordHitbox>().enemiesInHitbox)
        {
            enemy.DealDamage(damagePerSwing);
            ApplyKnockback(enemy);
        }
        attackBlocked = true;
        StartCoroutine(AttackCooldownStart());
    }

    public override void Passive()
    {
    }

    public IEnumerator AttackCooldownStart()
    {
        yield return new WaitForSeconds(attackDelay);
        attackBlocked = false;
    }
    private void ApplyKnockback(EnemyStateManager enemy)
    {
        Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
        if (enemyRb != null)
        {
            Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
            float knockbackStrength = 5f;

            enemyRb.velocity = Vector2.zero;
            enemyRb.AddForce(knockbackDirection * knockbackStrength, ForceMode2D.Impulse);

            StartCoroutine(ReduceKnockback(enemyRb));
        }
    }

    private IEnumerator ReduceKnockback(Rigidbody2D enemyRb)
    {
        float decayTime = 0.5f;
        int steps = 5;
        float waitTime = decayTime / steps;
        float decayFactor = 0.9f;

        for (int i = 0; i < steps; i++)
        {
            enemyRb.velocity *= decayFactor;
            yield return new WaitForSeconds(waitTime);
        }

        enemyRb.velocity = Vector2.zero;
    }

}
