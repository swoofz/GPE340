using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Pickup : MonoBehaviour {

    private Transform tf;       // Place to store our  transform

    [SerializeField] private float lifespan = 2f;       // Time object has before being destored
    [SerializeField] private float rotationSpeed = 5f;  // The rotation speed

    private float rotation = 0; // Value to update our rotation

    private void Awake() {
        // Initize Variables
        tf = GetComponent<Transform>();
    }

    private void Update() {
        // Have our object spinning
        rotation += ( rotationSpeed * Time.deltaTime );
        tf.rotation = Quaternion.Euler(0, rotation, 0);
    }

    private void OnTriggerEnter(Collider other) {
        // Get player
        Player character = other.GetComponent<Player>();

        if(character != null) {     // if got a player
            OnPickUp(character);    // Pick up something
        }
    }

    // Defualt method
    protected virtual void OnPickUp(Player player) {
        // Destory after given time 
        Destroy(gameObject, lifespan);
    }
}
