using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : AItem
{
    public bool attackBlocked = false;
    public float attackDelay = 1f;
    public float damagePerBullet = 15f;
    public GameObject bullet;

    public override void Use()
    {
    }

    public override void Attack()
    {
        if (attackBlocked)
            return;
        Debug.Log("Attacked!");
        attackBlocked = true;
        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
        WeaponParent weaponParent = transform.parent.gameObject.GetComponent<WeaponParent>();
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        newBullet.transform.localScale = new Vector3(.5f,.5f,.5f);
        newBullet.GetComponent<Bullet>().damage = damagePerBullet;
        rb.AddForce(weaponParent.direction * 15, ForceMode2D.Impulse);
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
