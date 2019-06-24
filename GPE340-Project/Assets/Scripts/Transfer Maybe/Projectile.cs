using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [HideInInspector] public float damage;

    private float lifespan = 5f;

    public Rigidbody rigidBody { get; private set; }

    private void Awake() {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnCollisionEnter(Collision collision) {
        Health player = collision.gameObject.GetComponent<Health>();
        if(player != null) {
            // Do damaage
        }

        Destroy(gameObject, lifespan);
    }
}
