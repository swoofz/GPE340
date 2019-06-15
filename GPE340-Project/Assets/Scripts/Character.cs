using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    private Transform tf;       // Place to store our transform

    void Awake() {
        tf = GetComponent<Transform>(); // Call Getcomponent once for our transform
    }

    // Function: MOVE
    public void Move(float x, float y) {
        tf.position += tf.forward * y * Time.deltaTime;
        tf.position += tf.right * x * Time.deltaTime;
    }

}
