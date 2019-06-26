using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    [SerializeField] private float respawnTimer = 0f;           // Time to respawn
    [SerializeField] private GameObject player = null;          // Player to respawn

    // Delete later it is just for showing
    [SerializeField] private float health = 0f; 

    private Collider BC;        // Be able tot get any collider on our object
    private Health Health;      // Get the health of our target

    private float timer;    // Timer that respawn after the time is up

    private void Awake() {
        // Set Initail Variables for easy access
        BC = GetComponent<Collider>();
        Health = GetComponent<Health>();
    }


    private void Update() {
        health = Health.health;         // Show health of our respawn target

        // Respawn after time
        timer -= Time.deltaTime;
        if(timer <= 0) {
            // Reactive our target and enable the collider
            BC.enabled = true;
            player.SetActive(true);
        }
    }

    void OnDie() {
        // Start timer and reset health;
        timer = respawnTimer;
        Health.Heal(Health.MaxHealth);
    }
}
