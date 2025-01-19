using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeadState : ACharacterBaseState
{
    private float fadeDuration = 2f; // Time in seconds for the fade-out
    private Material characterMaterial; // Reference to the character's material
    private GameObject gameOverUI;

    public override void EnterState(CharacterStateManager character)
    {
        Debug.Log("Character Entered State: Dead");

        Renderer renderer = character.GetComponent<Renderer>();
        if (renderer != null)
        {
            characterMaterial = renderer.material;
            character.StartCoroutine(FadeOut(character));
        }
        else
        {
            Debug.LogError("Renderer not found on the character!");
        }
    }

    public override void UpdateState(CharacterStateManager character)
    {

    }

    private IEnumerator FadeOut(CharacterStateManager character)
    {
        float fadeTimer = 0f;

        if (characterMaterial != null)
        {
            while (fadeTimer < fadeDuration)
            {
                fadeTimer += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeDuration);
                Color newColor = characterMaterial.color;
                newColor.a = alpha;
                characterMaterial.color = newColor;
                yield return null;
            }

            Color finalColor = characterMaterial.color;
            finalColor.a = 0f;
            characterMaterial.color = finalColor;

            gameOverUI = Camera.main.transform.GetChild(1).gameObject;
            gameOverUI.SetActive(true);
            character.gameObject.SetActive(false);
        }
    }
}
