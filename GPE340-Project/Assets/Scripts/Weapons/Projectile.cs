using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float damage { get; set; }  // bullet damage

    private float lifespan = 1.2f;      // Time to delay the destory 

    public Rigidbody rigidBody { get; private set; }

    private void Awake() {
        // Initialize Variables
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        // Get health component on our collision target
        Health player = collision.gameObject.GetComponent<Health>();
        if(player != null) {        // if have a health component 
            player.Damage(damage);  // do damage
        }

        Destroy(gameObject, lifespan);  // else destory after give time
    }
}
