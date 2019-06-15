using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeView : MonoBehaviour {

    public float sensitivity;   // How fast want the camera to move
    public float MaxLookUp;     // Max rotation distance to be able to look up
    public float MaxLookDown;   // Max rotation distance to be able to look down

    private Transform tf;   // Store our transform

    void Awake() {
        tf = GetComponent<Transform>();     // Get our Transform on awake
    }

    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;       // Hide Cursor
    }

    // Update is called once per frame
    void Update() {
        // Show Cursor when press Escape
        if(Input.GetKey(KeyCode.Escape)) {
            Cursor.lockState = CursorLockMode.None;
        }

        CameraLook();  // Camera Look Movement
    }

    // Function: CAMERA_LOOK
    // Move the Camera rotation around for the play view
    void CameraLook() {
        // Create a vector with our direction want to look
        Vector3 input = new Vector3(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"), 0f);

        // If not to far up or down
        if (tf.localEulerAngles.x >= (360 + MaxLookUp) || (tf.localEulerAngles.x <= MaxLookDown)) {
            // Rotate Camera up or down basic on mouse movement
            tf.localEulerAngles += new Vector3(-input.y * sensitivity, 0f, 0f);
        } else {
            if (tf.localEulerAngles.x > ( 360 + MaxLookUp - 90)) {          // Too High
                tf.localEulerAngles = new Vector3(MaxLookUp, 0f, 0f);       // Reset Position to be able to use mouse to move up or down again
            }

            if (tf.localEulerAngles.x < MaxLookDown + 90) {                     // Too low
                tf.localEulerAngles = new Vector3(MaxLookDown - 0.1f, 0f, 0f);  // Reset Position
            }
        }

        tf.parent.Rotate(new Vector3(0f, input.x * sensitivity, 0f));       // Rotate player and Camera in a circle
    }
}
