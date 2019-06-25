using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    [SerializeField] private float respawnTimer = 0f;
    [SerializeField] private GameObject player = null;

    // Delete later it is just for showing
    [SerializeField] private float health = 0f;

    private Collider BC;
    private Health Health;

    private float timer;

    private void Awake() {
        BC = GetComponent<Collider>();
        Health = GetComponent<Health>();
    }


    private void Update() {
        health = Health.health;

        timer -= Time.deltaTime;
        if(timer <= 0) {
            BC.enabled = true;
            player.SetActive(true);
        }
    }

    void OnDie() {
        timer = respawnTimer;
        Health.Heal(1000);
    }
}
