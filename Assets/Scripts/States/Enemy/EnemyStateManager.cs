using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : ServicesReferences
{
    // FSM stuff
    public AEnemyBaseState currentCharacterState;

    public EnemyIdleState idleState = new EnemyIdleState();
    public EnemyChasingState chasingState = new EnemyChasingState();
    public EnemyAttackingState attackingState = new EnemyAttackingState();

    // Enemy stuff
    public float Health = 100f;
    public float MaxHealth = 100f;
    public float Damage = 5f;
    public float Speed = 5f;
    public EEnemyType enemyType = EEnemyType.Melee;

    // Some useful variables.
    public GameObject target;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

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
        currentCharacterState = idleState;
        currentCharacterState.EnterState(this);
    }

    void Update()
    {
        // Update state.
        currentCharacterState.UpdateState(this);
    }

    public void SwitchState(AEnemyBaseState newState)
    {
        currentCharacterState = newState;
        currentCharacterState.EnterState(this);
    }

    public void DestroyEntity()
    {
        Destroy(this.gameObject);
    }
}
