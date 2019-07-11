using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {

    [SerializeField] private string firstLevelSceneName = "";       // Name of the first scene
    [SerializeField] private GameObject mainMenuPanel = null;
    [SerializeField] private GameObject settingsPanel = null;

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

    public void ShowSettingsPanel() {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void ShowMainMenePanel() {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

}
