using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponParent : MonoBehaviour
{
    public SpriteRenderer weaponRenderer;
    public Vector2 MousePointerPos;

    public CharacterStateManager CSM;

    public AItem currentItem;
    public Animator animator;

    private void Start()
    {
        CSM = transform.parent.GetComponent<CharacterStateManager>();
    }

    private void Update()
    {
        try
        {
            weaponRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
            animator = transform.GetChild(0).GetComponent<Animator>();
            MousePointerPos = CSM.mousePos;
            Vector2 direction = (MousePointerPos - (Vector2)transform.position).normalized;
            //                                  casteamos la posicion para hacerla un vector2.
            transform.right = direction;

            Vector2 scale = transform.localScale;

            if (direction.x < 0)
                scale.y = -1;
            else if (direction.x > 0)
                scale.y = 1;

            transform.localScale = scale;
            if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
                weaponRenderer.sortingOrder = -1;
            else
                weaponRenderer.sortingOrder = 0;
        }
        catch { }
    }

    public void PerformAttack(InputAction.CallbackContext obj)
    {
        animator.SetTrigger("Attack");
        currentItem.Attack();
    }
}
