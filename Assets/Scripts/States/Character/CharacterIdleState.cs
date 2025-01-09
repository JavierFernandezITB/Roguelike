using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdleState : ACharacterBaseState
{
    public override void EnterState(CharacterStateManager character)
    {
        Debug.Log("Character Entered State: Idle");
    }

    public override void UpdateState(CharacterStateManager character)
    {
        if (character.moveInput != Vector2.zero)
        {
            character.SwitchState(character.walkingState);
        }
        else
        {
            character.animator.SetBool("Walking", false);
        }
    }
}