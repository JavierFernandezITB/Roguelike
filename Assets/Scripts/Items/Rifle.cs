using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : AItem
{
    public bool attackBlocked = false;
    public float attackDelay = 1f;
    public float damagePerBullet = 15f;
    public GameObject bulletPrefab;

    private Queue<GameObject> bulletPool = new Queue<GameObject>();
    public int poolSize = 1;

    public override void Use()
    {
    }

    public override void Attack()
    {
        if (attackBlocked)
            return;

        attackBlocked = true;

        GameObject bullet = GetBulletFromPool();
        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.transform.localScale = new Vector3(.5f, .5f, .5f);
            bullet.SetActive(true);

            WeaponParent weaponParent = transform.parent.gameObject.GetComponent<WeaponParent>();
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            bullet.GetComponent<Bullet>().damage = damagePerBullet;
            bullet.GetComponent<Bullet>().rifle = this;
            bulletPool.Enqueue(bullet);
            rb.AddForce(weaponParent.direction * 15, ForceMode2D.Impulse);
        }

        StartCoroutine(AttackCooldownStart());
    }

    public void Attack(GameObject target)
    {
        GameObject bullet = GetBulletFromPool();
        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.transform.localScale = new Vector3(.5f, .5f, .5f);
            bullet.SetActive(true);

            Vector3 direction = ((Vector2)target.transform.position - (Vector2)transform.position).normalized;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            bullet.GetComponent<Bullet>().damage = damagePerBullet;
            bullet.GetComponent<Bullet>().isFromEnemy = true;
            bullet.GetComponent<Bullet>().rifle = this;
            rb.AddForce(direction * 15, ForceMode2D.Impulse);
        }
    }

    public override void Passive()
    {
    }

    public IEnumerator AttackCooldownStart()
    {
        yield return new WaitForSeconds(attackDelay);
        attackBlocked = false;
    }

    public GameObject GetBulletFromPool()
    {
        if (bulletPool.Count > 0)
        {
            return bulletPool.Dequeue();
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            return bullet;
        }
    }

    public void ReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
