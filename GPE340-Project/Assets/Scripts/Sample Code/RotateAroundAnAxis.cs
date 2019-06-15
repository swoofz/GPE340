using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundAnAxis : MonoBehaviour {

    public Vector3 axis = Vector3.up;
    public float rotationSpeed = 90f;

    private Vector3 lastLocation;
    private Vector3 newLocation;
    private float axisSpeed;

    // Start is called before the first frame update
    void Start() {
        lastLocation = Input.mousePosition;
        newLocation = lastLocation;
    }

    // Update is called once per frame
    void Update() {
        //RotateAround();
        Aim();
    }

    void RotateAround() {
        //Debug.Log(Input.mousePosition);
        //axis = new Vector3(0f, Input.mousePosition.x, 0f);
        newLocation = Input.mousePosition;

        axisSpeed = newLocation.x - lastLocation.x;
        axisSpeed *= rotationSpeed;
        transform.rotation *= Quaternion.AngleAxis(axisSpeed * Time.deltaTime, axis);

        lastLocation = newLocation;


        //transform.rotation *= Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, axis);
    }

    void Aim() {
        Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        if(plane.Raycast(ray, out distance)) {
            Debug.Log(ray.GetPoint(distance));
            //transform.position = ray.GetPoint(distance);
        }
    }
}
