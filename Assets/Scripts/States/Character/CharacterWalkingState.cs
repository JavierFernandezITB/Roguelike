using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterWalkingState : ACharacterBaseState
{
    private float oldHealth;

    public override void EnterState(CharacterStateManager character)
    {
        Debug.Log("Character Entered State: Walking");
    }

    public override void UpdateState(CharacterStateManager character)
    {
        if (character.moveInput != Vector2.zero)
        {
            character.animator.SetBool("Walking", true);
            character.animator.SetFloat("XInput", character.moveInput.x);
            character.animator.SetFloat("YInput", character.moveInput.y);
            character.transform.Translate(character.moveDirection * character.Speed * Time.deltaTime, Space.World);
        }
        else
        {
            character.SwitchState(character.idleState);
        }
    }
}