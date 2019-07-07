using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    static public UIManager Instance { get; private set; }

    [SerializeField] private HealthBar playerHealtthBar = null;
    [SerializeField] private Transform enemyHealthBarContainer = null;
    [SerializeField] private HealthBar enemyHealthBarPrefab = null;
    [SerializeField] private GameObject pauseMenu = null;
    [SerializeField] private GameObject loseMenu = null;

    private void Awake() {
        Instance = this;
    }


    public void RegisterPlayer(Player player) {
        playerHealtthBar.SetTarget(player.Health);
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

    public void ShowLoseMenu() {
        loseMenu.SetActive(true);
    }

    public void ButtonResume() {
        GameManager.UnPause();
    }
}
