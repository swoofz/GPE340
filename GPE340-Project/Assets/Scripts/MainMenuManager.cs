using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {

    [SerializeField] private string firstLevelSceneName = "";

    public void ButtonStart() {
        SceneManager.LoadScene(firstLevelSceneName);
    }

    public void ButtonQuit() {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}
