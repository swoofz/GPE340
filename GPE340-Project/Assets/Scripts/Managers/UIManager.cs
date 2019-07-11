using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour {

    static public UIManager Instance { get; private set; }

    // UI element that get stored in variable to change
    [SerializeField] private HealthBar playerHealtthBar = null; 
    [SerializeField] private StaminaBar playerStaminaBar = null;
    [SerializeField] private Transform enemyHealthBarContainer = null;
    [SerializeField] private HealthBar enemyHealthBarPrefab = null;
    [SerializeField] private GameObject pauseMenu = null;
    [SerializeField] private GameObject settingsMenu = null;
    [SerializeField] private GameObject loseMenu = null;
    [SerializeField] private GameObject winMenu = null;
    [SerializeField] private Image weaponDisplay = null;
    [SerializeField] private Text lifeText = null;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if(GameManager.Instance.playerPrefab) {
            if (GameManager.Instance.playerPrefab.equippedWeapon) {
                // Display weapon Icon
                weaponDisplay.overrideSprite = GameManager.Instance.playerPrefab.equippedWeapon.Icon;
                weaponDisplay.preserveAspect = true;
            }

            lifeText.text = string.Format("Lives: {0}", GameManager.Instance.Lives);
        }
    }


    public void RegisterPlayer(Player player) {
        playerHealtthBar.SetTarget(player.Health);
        playerStaminaBar.SetTarget(player);
    }

    public void RegisterEnemy(Enemy enemy) {
        HealthBar healthBar = Instantiate(enemyHealthBarPrefab) as HealthBar;
        healthBar.transform.SetParent(enemyHealthBarContainer, false);
        healthBar.SetTarget(enemy.Health);
    }

    public void ShowPauseMeun() {
        pauseMenu.SetActive(true);
    }

    public void HidePauseMenu() {
        pauseMenu.SetActive(false);
    }

    public void ShowSettingsMenu() {
        settingsMenu.SetActive(true);
    }

    public void HideSettingsMenu() {
        settingsMenu.SetActive(false);
    }

    public void ShowLoseMenu() {
        loseMenu.SetActive(true);
    }

    public void ShowWinMenu() {
        winMenu.SetActive(true);
    }

    public void ButtonResume() {
        GameManager.UnPause();
    }

    public void ButtonQuit() {
        GameManager.Quit();
    }

    public void ButtonContinue() {
        winMenu.SetActive(false);

        // Check if have another scene after this one
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            GameManager.Quit();
    }

    public void ButtonRestart() {
        // Restart the scene
        loseMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
