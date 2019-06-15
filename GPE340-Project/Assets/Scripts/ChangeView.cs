using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeView : MonoBehaviour {

    public float sensitivity;
    public float MaxLookUp;
    public float MaxLookDown;

    private Transform tf;

    void Awake() {
        tf = GetComponent<Transform>();
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

        CarmeraLook();
    }

    void CarmeraLook() {
        // Create a vector with our direction want to look
        Vector3 input = new Vector3(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"), 0f);

        tf.Rotate(new Vector3(-input.y * sensitivity, 0f, 0f));             // Rotate Camara Up and Down
        tf.parent.Rotate(new Vector3(0f, input.x * sensitivity, 0f));       // Rotate player and Camara in a circle
    }
}
