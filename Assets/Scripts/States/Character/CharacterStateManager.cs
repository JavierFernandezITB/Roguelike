using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterStateManager : ServicesReferences
{
    // FSM stuff
    public ACharacterBaseState currentCharacterState;

    public CharacterIdleState idleState = new CharacterIdleState();
    public CharacterWalkingState walkingState = new CharacterWalkingState();
    public CharacterHurtState hurtState = new CharacterHurtState();
    public CharacterDeadState deadState = new CharacterDeadState();

    // Character stuff
    public float Health = 100f;
    public float MaxHealth = 100f;
    public float Speed = 5.0f;

    // Some useful variables.
    public Animator animator;

    public SpriteRenderer spriteRenderer;
    public WeaponParent weaponParent;
    public Vector2 moveInput;
    public Vector3 moveDirection;
    public Vector3 mousePos;
    public CharacterControls controls;
    public Image uiHealth;
    public AudioClip hurtSound;

    // Code lol

    void Awake()
    {
        base.GetServices();
        base.Persist<CharacterStateManager>();

        //Setup
        weaponParent = transform.GetChild(0).GetComponent<WeaponParent>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        controls = new CharacterControls();
        controls.Enable();
    }

    void Start()
    {
        //FSM
        currentCharacterState = idleState;
        currentCharacterState.EnterState(this);
    }

    void Update()
    {
        // Local Character Variables.
        moveInput = controls.Character.Move.ReadValue<Vector2>();
        moveDirection = new Vector2(moveInput.x, moveInput.y).normalized;
        mousePos = GetPointerInput();



        // Ui updates.
        UpdateUIHealth();

        // Update state.
        currentCharacterState.UpdateState(this);
    }

    private void OnEnable()
    {
        controls.Character.Attack.performed += weaponParent.PerformAttack;
    }

    private void OnDisable()
    {
        controls.Character.Attack.performed -= weaponParent.PerformAttack;
    }

    public Vector2 GetPointerInput()
    {
        mousePos = controls.Character.MousePointerPosition.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    public void SwitchState(ACharacterBaseState newState)
    {
        currentCharacterState = newState;
        currentCharacterState.EnterState(this);
    }

    public void DealDamage(float amount)
    {
        if (currentCharacterState == hurtState)
            return;
        Health -= amount;
        if (Health > 0)
            SwitchState(hurtState);
        else
            SwitchState(deadState);
    }

    public void UpdateUIHealth()
    {
        uiHealth.fillAmount = Health / 100f;
    }
}
