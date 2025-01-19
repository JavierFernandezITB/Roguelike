using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : AItem
{
    public bool attackBlocked = false;
    public float attackDelay = 1f;
    public float damagePerHitdelay = 2.5f;
    public bool attackDebounce = false;
    public float hitDelay = .5f;
    public Animator flamethrowerAnimator;

    public void Start()
    {
        flamethrowerAnimator = transform.GetComponent<Animator>();
    }

    public override void Use()
    {
    }

    public override void Attack()
    {
        if (attackBlocked)
            return;
        Debug.Log("Attacked!");
        attackBlocked = true;
        attackDebounce = !attackDebounce;
        StartCoroutine(AttackSwitch());
        StartCoroutine(AttackCooldownStart());
    }

    public override void Passive()
    {
    }

    public IEnumerator AttackSwitch()
    {
        flamethrowerAnimator.SetBool("isAttacking", true);
        while (attackDebounce)
        {
            try
            {
                foreach (EnemyStateManager enemy in transform.GetChild(0).GetComponent<FlamethrowerHitbox>().enemiesInHitbox)
                {
                    enemy.DealDamage(damagePerHitdelay);
                }
            }
            catch { }
            
            yield return new WaitForSeconds(hitDelay);
        }
        flamethrowerAnimator.SetBool("isAttacking", false);
    }

    public IEnumerator AttackCooldownStart()
    {
        yield return new WaitForSeconds(attackDelay);
        attackBlocked = false;
    }
}
