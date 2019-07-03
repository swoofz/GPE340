using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }
    public float Lives { get { return lives; } }

    [Header("Level Settings")]
    public Player playerPrefab;
    public Transform playerSpawnPoint;
    public float playerRespawnDelay = 3f;
    static public bool Paused = false;

    [SerializeField] private int lives = 3;

    private void Awake() {
        if (!Instance) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
            
    }

    // Start is called before the first frame update
    void Start() {
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Paused) {
                UnPause();
            } else {
                Pause();
            }
        }
    }

    private void SpawnPlayer() {
        Player player = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation) as Player;
        playerPrefab = player;
        player.Health.Heal(playerPrefab.Health.MaxHealth);
        player.ragController.TurnOffElementsIncludingChildren();
        player.enabled = true;

        playerPrefab.Health.OnDie.AddListener(HandlePlayerDeath);
    }

    private void HandlePlayerDeath() {
        playerPrefab.Health.OnDie.RemoveListener(HandlePlayerDeath);

        if(lives > 0) {
            Invoke("SpawnPlayer", playerRespawnDelay);
            lives--;
        } else {
            //  GameOver
        }
    }

    public static void Pause() {
        Paused = true;
        Time.timeScale = 0;
    }

    public static void UnPause() {
        Paused = false;
        Time.timeScale = 1;
    }
}
