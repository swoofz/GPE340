using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }
    public float Lives { get { return lives; } }

    [SerializeField] private UnityEvent onPause = null;     // Events when paused
    [SerializeField] private UnityEvent onResume = null;    // Events when unpaused
    [SerializeField] private UnityEvent onLose = null;      // Events when lose
    [SerializeField] private UnityEvent onWin = null;       // Events when win
    [SerializeField] private string mainMenuSceneName = ""; // Main menu scene name

    [Header("Level Settings")]
    public Player playerPrefab;
    public Transform playerSpawnPoint;
    public float playerRespawnDelay = 3f;
    static public bool Paused = false;

    [SerializeField] private int lives = 3;     // Player's lives

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        // Spawn a player at the start
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update() {
        // Check if the player has won
        if (CheckWinCondition()) {
            // Stop player from being able to do anything and show win menu
            StopPlayerActions();
            onWin.Invoke();
        }

        // Pause or unpause the game
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Paused) {
                UnPause();
            } else {
                Pause();
            }
        }
    }

    private void SpawnPlayer() {
        // Spawn a player with full health
        Player player = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation) as Player;
        playerPrefab = player;
        player.Health.Heal(playerPrefab.Health.MaxHealth);
        player.ragController.TurnOffElementsIncludingChildren();
        player.enabled = true;

        playerPrefab.Health.OnDie.AddListener(HandlePlayerDeath);
    }

    private void HandlePlayerDeath() {
        // Handle how many lives a player has left
        playerPrefab.Health.OnDie.RemoveListener(HandlePlayerDeath);

        if(lives > 0) {
            Invoke("SpawnPlayer", playerRespawnDelay);
            lives--;
        } else {
            onLose.Invoke();
        }
    }

    private bool CheckWinCondition() {
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();

        // Check if on last wave
        if (spawner.currentWave < spawner.MaxNumberOfWaves) // if not
            return false;

        // All units spawned and are dead
        if (spawner.LastMaxEnemies <= spawner.spawnedEnemyCount)
            if(spawner.currentActiveEnemies != 0) // if not
                return false;

        // True if last wave, all enemies spawned and are dead
        return true;
    }
    
    private void StopPlayerActions() {
        // Stop player from being able to do anything
        playerPrefab.Animator.enabled = false;
        playerPrefab.equippedWeapon.ReleaseTrigger();
        playerPrefab.enabled = false;
    }

    public static void Pause() {
        // Pause the game
        Paused = true;
        Time.timeScale = 0;
        Instance.onPause.Invoke();
    }

    public static void UnPause() {
        // Unpuase the game
        Paused = false;
        Time.timeScale = 1;
        Instance.onResume.Invoke();
    }

    public static void Quit() {
        // Quit game but go to main menu
        Paused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(Instance.mainMenuSceneName);
    }

    
}
