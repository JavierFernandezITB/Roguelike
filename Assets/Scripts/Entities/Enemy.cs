using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ServicesReferences, IDamageable
{
    public int Health = 100;
    public int MaxHealth = 100;
    public int Stamina = 20;
    public float Speed = 5.0f;
    public GameObject HeldItem;

    void Awake()
    {
        base.GetServices();
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
}
