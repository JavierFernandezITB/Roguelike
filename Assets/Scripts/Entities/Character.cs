using UnityEngine;
using UnityEngine.UI;

public class Character : ServicesReferences, IDamageable
{
    public int Health = 100;
    public int MaxHealth = 100;
    public int Stamina = 20;
    public float Speed = 5.0f;
    public GameObject HeldItem;

    public Animator animator;

    private Vector2 moveInput;
    private Vector3 moveDirection;
    private CharacterControls controls;

    void Awake()
    {
        base.GetServices();
        base.Persist<Character>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        controls = new CharacterControls();
        controls.Enable();
    }

    void Update()
    {
        HandleMovement();
        entityManagerService.Move(transform, moveDirection, Speed);
    }

    void HandleMovement()
    {
        moveInput = controls.Character.Move.ReadValue<Vector2>();
        if (moveInput != Vector2.zero)
        {
            animator.SetBool("Walking", true);
            animator.SetFloat("XInput", moveInput.x);
            animator.SetFloat("YInput", moveInput.y);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
        moveDirection = new Vector2(moveInput.x, moveInput.y).normalized;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
}
