using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : ServicesReferences
{
    // FSM stuff
    public AEnemyBaseState currentEnemyState;

    public EnemyIdleState idleState = new EnemyIdleState();
    public EnemyChasingState chasingState = new EnemyChasingState();
    public EnemyAttackingState attackingState = new EnemyAttackingState();
    public EnemyHurtState hurtState = new EnemyHurtState();
    public EnemyDeadState deadState = new EnemyDeadState();

    // Enemy stuff
    public float Health = 100f;
    public float MaxHealth = 100f;
    public float Damage = 5f;
    public float Speed = 5f;
    public float Range = 5f;
    public int MinimumCoinReward = 5;
    public int MaximumCoinReward = 15;
    public EEnemyType enemyType = EEnemyType.Melee;

    // Some useful variables.
    public RoomHandler currentRoomHandler;
    public GameObject target;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public AudioClip hurtSound;

    // Code lol

    void Awake()
    {
        base.GetServices();

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        //FSM
        currentEnemyState = idleState;
        currentEnemyState.EnterState(this);
    }

    void Update()
    {
        // Update state.
        currentEnemyState.UpdateState(this);
    }

    public void SwitchState(AEnemyBaseState newState)
    {
        currentEnemyState = newState;
        currentEnemyState.EnterState(this);
    }

    public void DealDamage(float amount)
    {
        if (currentEnemyState == hurtState || currentEnemyState == attackingState)
            return;
        Health -= amount;
        if (Health <= 0)
            SwitchState(deadState);
        else
            SwitchState(hurtState);
    }

    public GameObject SpawnDrop(GameObject prefab)
    {
        return Instantiate(prefab);
    }

    public void DestroyEntity()
    {
        Destroy(this.gameObject);
    }
}
