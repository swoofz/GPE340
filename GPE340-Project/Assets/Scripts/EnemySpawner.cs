using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public Enemy[] enemyUnits;

    [SerializeField] private int maxActiveEnemies = 5;
    [SerializeField] private int maxEnemies = 50;
    [SerializeField] private float spawnDelay = 3f;

    private List<Transform> spawnPoints;
    private int currentActiveEnemies = 0;
    private int spawnedEnemyCount = 0;


    private void Awake() {
        spawnPoints = new List<Transform>();
        foreach (Transform child in transform) {
            spawnPoints.Add(child);
        }
    }

    // Start is called before the first frame update
    void Start() {
        InvokeRepeating("SpawnEnemy", 0f, spawnDelay);
    }

    // Update is called once per frame
    void Update() {
        if (spawnedEnemyCount >= maxEnemies)
            CancelInvoke("SpawnEnemy");
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
}
