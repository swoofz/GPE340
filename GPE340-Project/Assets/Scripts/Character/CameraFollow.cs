using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float maxViewDistance = 20f;     // Max Distance want our camera away from target

    private Transform target = null;        // Target we want our camera to follow


    private void LateUpdate() {
        // Get Player if there is a player
        Player player = GameManager.Instance.playerPrefab;

        if (player)
            target = player.transform;
        
        // Create a new Vector3 and only change our x and z axis
        if (target) {
            Vector3 newTargetLocation = new Vector3(target.localPosition.x, 0f, target.position.z);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, newTargetLocation, maxViewDistance);
        }
    }
}
