using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignRotation : MonoBehaviour {

    private Transform tf;

    //[SerializeField, Tooltip("The Transform to mathc rotation with")]
    //private Transform target = null;
    //[SerializeField, Tooltip("Tje speed to mathc the target's rotation")]
    //private float speed = 5f;

    private void Awake() {
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {
        //tf.rotation = Quaternion.Slerp(tf.rotation, target.rotation, speed * Time.deltaTime);
    }
}
