using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameOver : MonoBehaviour
{
    public void GameOverExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
