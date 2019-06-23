using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Pickup : MonoBehaviour {

    private Transform tf;

    [SerializeField] private float lifespan = 2f;
    [SerializeField] private float rotationSpeed = 5f;

    private float rotation = 0;

    private void Awake() {
        tf = GetComponent<Transform>();
    }

    private void Update() {
        rotation += ( rotationSpeed * Time.deltaTime );
        tf.rotation = Quaternion.Euler(0, rotation, 0);
    }

    private void OnTriggerEnter(Collider other) {
        Player character = other.GetComponent<Player>();

        if(character != null) {
            OnPickUp(character);
        }
    }

    protected virtual void OnPickUp(Player player) {
        Destroy(gameObject, lifespan);
    }
}
