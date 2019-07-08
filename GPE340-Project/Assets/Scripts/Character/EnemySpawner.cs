using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public Enemy[] enemyUnits;      // Enemies that will get spawned

    // Getter Variables for other scripts
    public int currentWave { get; private set; }
    public int MaxNumberOfWaves { get { return waves.Length; } }
    public int LastMaxEnemies { get { return waves[waves.Length-1]; } }
    public int currentActiveEnemies { get; private set; }                   // Tracker of the active enemies in the scene
    public int spawnedEnemyCount { get; private set; }                      // Tracker for how many enemies spawn

    [Header("Wave Settings")]
    [SerializeField] private int maxActiveEnemies = 5;  // Max number of enemies that can be in the scene at once
    [SerializeField] private float spawnDelay = 3f;     // Delay after one enemy spawns 
    [SerializeField] private float tillNextWave = 5f;   // Time to wait till the next wave of enemies start spawning

    [Tooltip("# of Waves and # of Enenies per wave")]
    [SerializeField] private int[] waves = null;

    private List<Transform> spawnPoints;    // Location in with enemies spawn
    private float nextWaveTime = 0;         // Timer variable

    private void Awake() {
        // Get all spawn point locations
        spawnPoints = new List<Transform>();
        foreach (Transform child in transform) {
            spawnPoints.Add(child);
        }
    }

    // Start is called before the first frame update
    void Start() {
        // Run method multiple times
        InvokeRepeating("EnemyWaves", 0f, spawnDelay);
    }

    // Update is called once per frame
    void Update() {
        if (currentWave >= waves.Length)
            // Advance pass the number of wave to stop running method
            CancelInvoke("EnemyWaves");
        else {
            // Check if spawned all enemies have spawned
            if (spawnedEnemyCount >= waves[currentWave]) {
                // Set next spawn time for next wave
                nextWaveTime = Time.time + tillNextWave;

                // if there are no enemies in the scene then go to next wave
                if (currentActiveEnemies == 0) {
                    spawnedEnemyCount = 0;
                    currentWave++;
                }
            }
        }
    }


    private void SpawnEnemy() {
        if (currentActiveEnemies >= maxActiveEnemies)
            return;

        // Spawn an enmey
        currentActiveEnemies++;
        MakeEnemy();
    }

    private void HandleEnemyDeath() {
        // Tracking active enemies
        currentActiveEnemies--;
    }

    private void MakeEnemy() {
        // Get or make storage for our enemies
        GameObject enemyStorage = GameObject.Find("Enemies");
        if(!enemyStorage)
            enemyStorage = new GameObject("Enemies");

        // Spawn random enemies at random spawn point
        Enemy enemy = Instantiate(enemyUnits[Random.Range(0, enemyUnits.Length)],
            spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
        enemy.transform.SetParent(enemyStorage.transform);

        // Track enemies
        enemy.Health.OnDie.AddListener(HandleEnemyDeath);
        spawnedEnemyCount++;
    }

    private void EnemyWaves() {
        // Return if current wave exceeds how many waves we have
        if (currentWave > waves.Length)
            return;

        // Spawn this wave after some time
        if(Time.time > nextWaveTime) {
            SpawnEnemy();
        }

    }
}
