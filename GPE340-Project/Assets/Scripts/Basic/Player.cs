﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : WeaponAgent {

    public Health Health { get; private set; }
    public float StaminaPercentage { get { return stamina / maxStamina; } }

    [SerializeField, Tooltip("The max speed of the player")]
    private float speed = 4f;
    [SerializeField, Tooltip("See the player current Health. NOTE: NOT A CHANGABLE VALUE")]
    private float currentHealth;

    private Transform tf;

    private float currentSpeed;
    private float maxStamina = 100f;
    private float stamina;
    private bool walking = true;
    private bool resting = false;
    private bool moving = false;

    private void Awake() {
        tf = GetComponent<Transform>();
        SetAwakeVaribles();
        Health = GetComponent<Health>();
        stamina = maxStamina;
    }

    // Start is called before the first frame update
    void Start() {
        currentSpeed = speed;
    }

    // Update is called once per frame
    void Update() {
        currentHealth = Health.health;
        Move();
        Rotate();
        NeedToRest();

        if (!equippedWeapon) {
            EquipWeapon(startWeapon);
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            currentSpeed = speed * 2f;
            walking = false;
        } else {
            currentSpeed = speed;
            walking = true;
        }
    }

    void Move() {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1f);
        tf.position += input * currentSpeed * Time.deltaTime;
        input = tf.InverseTransformDirection(input * Sprint());
        Animator.SetFloat("Horizontal", input.x);
        Animator.SetFloat("Vertical", input.z);

        IsMoving(input.x, input.z);
    }

    void Rotate() {
        Plane plane = new Plane(Vector3.up, tf.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                            // Get a ray from our mouse position on the screen
        float distance;

        // From our raycast we can rotate our character to look in that direction
        if (plane.Raycast(ray, out distance)) {
            tf.rotation = Quaternion.LookRotation(ray.GetPoint(distance) - tf.position);
        }
    }

    int Sprint() {
        if (!walking && stamina > 0) {
            // Sprinting
            if (!resting && moving) {
                stamina -= 10 * Time.deltaTime;
                return 4;
            }
        }

        // Walking
        stamina += 10 * Time.deltaTime;
        if (stamina >= maxStamina) {
            stamina = maxStamina;
        }
        return 1;   
    }

    void NeedToRest() {
        if(stamina <= 0) {
            resting = true;
        }

        if(stamina > 30) {
            resting = false;
        }
    }

    void IsMoving(float x, float z) {
        if (x != 0 || z != 0) {
            moving = true;
        } else {
            moving = false;
        }
    }
}
