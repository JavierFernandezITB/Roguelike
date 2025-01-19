using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthbar : MonoBehaviour
{
    void Update()
    {
        UpdateHealthBarSize();
    }

    private void UpdateHealthBarSize()
    {
        float healthPercentage = transform.parent.GetComponent<EnemyStateManager>().Health / transform.parent.GetComponent<EnemyStateManager>().MaxHealth;
        Vector3 newScale = transform.localScale;
        newScale.x = healthPercentage;
        transform.localScale = newScale;
    }
}
