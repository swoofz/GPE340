using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {

    [SerializeField] private string firstLevelSceneName = "";       // Name of the first scene

    public void ButtonStart() {
        // Load first scene
        SceneManager.LoadScene(firstLevelSceneName);
    }

    public void ButtonQuit() {
        // Close application
        Application.Quit();

        // If in the editor stop playing
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}
