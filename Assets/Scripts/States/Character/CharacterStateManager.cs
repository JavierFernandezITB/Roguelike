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

    // Character stuff
    public float Health = 100f;
    public float MaxHealth = 100f;
    public float Speed = 5.0f;

    // Some useful variables.
    public Animator animator;

    public Vector2 moveInput;
    public Vector3 moveDirection;
    public CharacterControls controls;
    public Image uiHealth;

    // Code lol

    void Awake()
    {
        base.GetServices();
        base.Persist<Character>();
    }

    void Start()
    {
        //Setup
        animator = GetComponent<Animator>();
        controls = new CharacterControls();
        controls.Enable();

        //FSM
        currentCharacterState = idleState;
        currentCharacterState.EnterState(this);
    }

    void Update()
    {
        // Local Character Variables.
        moveInput = controls.Character.Move.ReadValue<Vector2>();
        moveDirection = new Vector2(moveInput.x, moveInput.y).normalized;

        // Ui updates.
        UpdateUIHealth();

        // Update state.
        currentCharacterState.UpdateState(this);
    }

    public void SwitchState(ACharacterBaseState newState)
    {
        currentCharacterState = newState;
        currentCharacterState.EnterState(this);
    }

    public void UpdateUIHealth()
    {
        uiHealth.fillAmount = Health / 100f;
    }
}
