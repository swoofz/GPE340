using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float maxViewDistance = 30f;     // Max Distance want our camera away from target

    [SerializeField]
    private Transform target = null;               // Target we want our camera to follow


    private void LateUpdate() {
        // Create a new Vector3 and only change our x and z axis
        if (target) {
            Vector3 newTargetLocation = new Vector3(target.localPosition.x, 10f, target.position.z - 10f);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, newTargetLocation, maxViewDistance);
        }
    }
}
