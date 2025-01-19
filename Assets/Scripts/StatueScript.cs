using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatueScript : ServicesReferences
{
    public string sceneTarget;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneTarget);
            GameObject character = GameObject.Find("/Character");
            character.transform.position = new Vector3(0, 0, 0);
            Camera.main.transform.position = new Vector3(0, 0, -20);
        }
    }
}
