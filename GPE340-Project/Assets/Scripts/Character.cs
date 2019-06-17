using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterData))]
public class Character : MonoBehaviour {

    private float waitTimer;        // Timer used to not be able to sprint right aways after use all stamina
    private Transform tf;           // Place to store our transform
    private Animator animator;      // Get our animator
    private CharacterData charData; // Get this Character data to use

    void Awake() {
        // Set up variable to store components before start
        tf = GetComponent<Transform>();
        animator = GetComponent<Animator>(); 
        charData = GetComponent<CharacterData>();
    }

    // Function: Move
    // Movement for our player
    public  void Move() {
        // Don't even run if don't have an animator
        if (animator == null) return;

        // Set our movement in a new Vector3
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        input = Vector3.ClampMagnitude(input, 1f);                          // Even our vector to not allow for going faster with two inputs
        input = tf.InverseTransformDirection(input * charData.speed);       // Update our position in world space
        IsMoving(input.x, input.z);                                         // Define if we are moving

        // Set our animations
        animator.SetFloat("Horizontal", input.x); 
        animator.SetFloat("Vertical", input.z);
        charData.speed = 1; // Reset speed
    }

    // Function: Rotate
    // Rotate our player around to be able to look another direction
    public void Rotate() {
        Plane plane = new Plane(Vector3.up, tf.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                            // Get a ray from our mouse position on the screen
        float distance;
        
        // From our raycast we can rotate our character to look in that direction
        if (plane.Raycast(ray, out distance)) {
            tf.rotation = Quaternion.LookRotation(ray.GetPoint(distance) - tf.position);
        }
    }

    // Function: Sprint
    // Make player run faster
    public void Sprint() {
        // Change character speed
        charData.speed = 2;
        if (charData.moving) {
            // Lose stamina the longer our character is sprinting
            charData.stamina -= 10 * Time.deltaTime;
        } else {
            RestoreStamina();
        }
    }

    // Function: Restore_Stamina
    // Restore stamine over time back up to max
    public void RestoreStamina() {
        waitTimer -= Time.deltaTime;                    // If have a timer going
        if (charData.stamina < 100) {                   // As long a not max stamina increase the amount
            charData.stamina += 5 * Time.deltaTime;
        } else {
            charData.stamina = 100;                     // At max so set to max
        }
    }

    // Function: Is_Sprinting
    // Check if can sprint
    public bool IsSprinting() {
        if(waitTimer <= 0 && charData.stamina > 0) {    // Have stamina
            return true;                                // Return true
        }

        // Make a wait timer after use all stamina
        if(charData.stamina <= 0) {
            waitTimer = 5f;
        }

        // No stamina return false
        return false;
    }

    // Function: Is_Moving
    // Check if our character is moving or not
    void IsMoving(float x, float z) {
        // As long a x or y doesn't equal zero are character is moving
        if (x != 0 || z != 0) {
            charData.moving = true;
        } else {
            charData.moving = false;
        }
    }
}
