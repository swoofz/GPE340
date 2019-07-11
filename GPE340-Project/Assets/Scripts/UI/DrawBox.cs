using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBox : MonoBehaviour {

    public Vector3 scale;
    public Color color = Color.cyan;

    // In the scene view Draw a Gizmos
    private void OnDrawGizmos() {
        // Used to see Spawn locations in scene
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.color = Color.Lerp(color, Color.clear, 0.35f);
        Gizmos.DrawCube(Vector3.up * scale.y / 2f, scale);
        Gizmos.color = color;
        Gizmos.DrawRay(Vector3.zero, Vector3.forward * 0.4f);
    }
}
