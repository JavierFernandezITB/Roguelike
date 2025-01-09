using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    GameObject character;

    private void Awake()
    {
        character = GameObject.Find("/Character");
    }

    private void FixedUpdate()
    {
        if (character != null)
        {
            transform.position = new Vector3(character.transform.position.x, character.transform.position.y, transform.position.z);
        }
    }
}
