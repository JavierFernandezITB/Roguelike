using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHurtState : ACharacterBaseState
{
    private float hurtDelay = 0.25f;
    public Color hurtColor = Color.red; // Color when hurt
    private Color originalColor; // Store the original color of the sprite
    private AudioSource audioSource;

    public override void EnterState(CharacterStateManager character)
    {
        Debug.Log("Character Entered State: Hurt");
        originalColor = character.spriteRenderer.color;
        character.spriteRenderer.color = hurtColor;
        if (audioSource == null)
            audioSource = character.GetComponent<AudioSource>();
        if (character.hurtSound != null)
            audioSource.PlayOneShot(character.hurtSound);
        character.StartCoroutine(HurtDelay(character));
    }

    public override void UpdateState(CharacterStateManager character)
    {

    }

    private IEnumerator HurtDelay(CharacterStateManager character)
    {
        Debug.Log($"Player got hurt! Waiting {hurtDelay}s before moving.");
        yield return new WaitForSeconds(hurtDelay);
        character.spriteRenderer.color = originalColor;
        character.SwitchState(character.idleState);
    }
}
