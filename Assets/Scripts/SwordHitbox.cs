using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public List<EnemyStateManager> enemiesInHitbox = new List<EnemyStateManager>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyStateManager ESM = other.GetComponent<EnemyStateManager>();
        if (ESM != null)
        {
            enemiesInHitbox.Add(ESM);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EnemyStateManager ESM = other.GetComponent<EnemyStateManager>();
        if (ESM != null)
        {
            if (enemiesInHitbox.Contains(ESM))
            {
                enemiesInHitbox.Remove(ESM);
            }
        }
    }
}
