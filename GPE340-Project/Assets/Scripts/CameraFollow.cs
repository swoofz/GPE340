using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float maxViewDistance = 30f;

    private void LateUpdate() {
        Vector3 newTargetLocation = new Vector3(target.localPosition.x, 10f, target.position.z - 10f);
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, newTargetLocation, maxViewDistance);
    }
}
