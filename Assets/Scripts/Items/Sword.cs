using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : AItem
{
    public bool attackBlocked = false;
    public float attackDelay = 1f;

    public override void Use()
    {
    }

    public override void Attack()
    {
        if (attackBlocked)
            return;
        Debug.Log("Attacked!");
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
}
