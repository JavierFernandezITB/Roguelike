using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage; // The damage the bullet will deal

    private void OnTriggerEnter2D(Collider2D collider)
    {
        EnemyStateManager enemy = collider.GetComponent<EnemyStateManager>();
        if (enemy != null)
        {
            enemy.DealDamage(damage);
            Destroy(gameObject);
        } else if (collider.gameObject.layer != 8)
        {
            Debug.Log(collider.gameObject.layer);
            Destroy(gameObject);
        }
    }
}

