using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public bool isFromEnemy = false;
    public Rifle rifle;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isFromEnemy)
        {
            EnemyStateManager enemy = collider.GetComponent<EnemyStateManager>();
            if (enemy != null)
            {
                enemy.DealDamage(damage);
                ReturnToPool();
            }
            else if (collider.gameObject.layer != 8)
            {
                Debug.Log(collider.gameObject.layer);
                ReturnToPool();
            }
        }
        else
        {
            CharacterStateManager character = collider.GetComponent<CharacterStateManager>();
            if (character != null)
            {
                character.DealDamage(damage);
                ReturnToPool();
            }
            else if (collider.gameObject.layer != 8)
            {
                Debug.Log(collider.gameObject.layer);
                ReturnToPool();
            }
        }
    }

    private void ReturnToPool()
    {
        if (rifle != null)
        {
            rifle.ReturnBulletToPool(gameObject);
        }
        else
        {
            Debug.LogWarning("Rifle reference not found. Bullet cannot be returned to pool.");
            gameObject.SetActive(false);
        }
    }
}
