using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public Enemy[] enemyUnits;
    public int currentWave { get; private set; }
    public int MaxNumberOfWaves { get { return waves.Length; } }

    [Header("Wave Settings")]
    [SerializeField] private int maxActiveEnemies = 5;
    [SerializeField] private float spawnDelay = 3f;
    [SerializeField] private float tillNextWave = 5f;

    [Tooltip("# of Waves and # of Enenies per wave")]
    [SerializeField] private int[] waves = null;

    private List<Transform> spawnPoints;
    private int currentActiveEnemies = 0;
    private int spawnedEnemyCount = 0;
    private float nextWaveTime = 0;

    private void Awake() {
        spawnPoints = new List<Transform>();
        foreach (Transform child in transform) {
            spawnPoints.Add(child);
        }
    }

    // Start is called before the first frame update
    void Start() {
        InvokeRepeating("EnemyWaves", 0f, spawnDelay);
    }

    // Update is called once per frame
    void Update() {
        if (currentWave >= waves.Length)
            CancelInvoke("EnemyWaves");
        else {
            if (spawnedEnemyCount >= waves[currentWave]) {
                nextWaveTime = Time.time + tillNextWave;

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
        currentActiveEnemies--;
    }

    private void MakeEnemy() {
        GameObject enemyStorage = GameObject.Find("Enemies");
        if(!enemyStorage)
            enemyStorage = new GameObject("Enemies");

        Enemy enemy = Instantiate(enemyUnits[Random.Range(0, enemyUnits.Length)],
            spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
        enemy.transform.SetParent(enemyStorage.transform);
        enemy.Health.OnDie.AddListener(HandleEnemyDeath);
        spawnedEnemyCount++;
    }

    private void EnemyWaves() {
        if (currentWave > waves.Length)
            return;

        if(Time.time > nextWaveTime) {
            SpawnEnemy();
        }

    }
}
