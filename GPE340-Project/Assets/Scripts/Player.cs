using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : WeaponAgent {

    public Health Health { get; private set; }
    public float StaminaPercentage { get { return stamina / maxStamina; } }

    public Weapon startWeapon;      // Starting weapon that will be equipped on our player

    [SerializeField, Tooltip("The max speed of the player")]
    private float speed = 4f;
    [SerializeField, Tooltip("See the player current Health. NOTE: NOT A CHANGABLE VALUE")]
    private float currentHealth;

    private Transform tf;           // Our transform store variable

    private RagdollController ragController;

    private float currentSpeed;         // change in speed
    private float maxStamina = 100f;    // max stamina to be able to sprint
    private float stamina;              // stamina that is used for sprinting
    private bool walking = true;        // are we walking?
    private bool resting = false;       // are we sprinting?
    private bool moving = false;        // are we moving?

    protected override void Awake() {
        // Initailize Variables
        base.Awake();
        tf = GetComponent<Transform>();
        Health = GetComponent<Health>();
        ragController = GetComponent<RagdollController>();
        stamina = maxStamina;
        EquipWeapon(startWeapon);
    }

    // Start is called before the first frame update
    void Start() {
        currentSpeed = speed;
    }

    // Update is called once per frame
    void Update() {
        currentHealth = Health.health;  // Update our show current health value
        Move();
        Shoot();
        Rotate();
        NeedToRest();

        // Left shift to sprint
        if (Input.GetKey(KeyCode.LeftShift)) {
            currentSpeed = speed * 2f;
            walking = false;
        } else {
            currentSpeed = speed;
            walking = true;
        }
    }

    void Move() {
        // Set our movement in a new Vector3
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        input = Vector3.ClampMagnitude(input, 1f);                  // Even our vector, so cant go faster diagonal
        tf.position += input * currentSpeed * Time.deltaTime;       // Control our speed
        input = tf.InverseTransformDirection(input * Sprint());     // Update our position in world space

        // Set our animations
        Animator.SetFloat("Horizontal", input.x);
        Animator.SetFloat("Vertical", input.z);

        IsMoving(input.x, input.z); // Define if we are moving
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

    // Return the change animation value
    int Sprint() {
        // Make sure we not walking and have stamina to run with
        if (!walking && stamina > 0) {
            // Sprinting
            if (!resting && moving) {
                stamina -= 10 * Time.deltaTime;
                return 4;
            }
        }

        // Walking & Resting  --- Restore stamina to full
        stamina += 10 * Time.deltaTime;
        if (stamina >= maxStamina) {
            stamina = maxStamina;
        }
        return 1;   
    }

    // Stamina cooldown if used all
    void NeedToRest() {
        if(stamina <= 0) {
            resting = true;
        }

        if(stamina > 30) {
            resting = false;
        }
    }

    // Check if we are moving
    void IsMoving(float x, float z) {
        // As long as x or z doesn't equal zero are character is moving
        if (x != 0 || z != 0) {
            moving = true;
        } else {
            moving = false;
        }
    }

    void Shoot() {
        // Mouse button 0 is our shoot button
        if (Input.GetMouseButton(0)) {
            equippedWeapon.PullTrigger();
        } else {
            equippedWeapon.ReleaseTrigger();
        }
    }

    void OnDie() {
        Unequip();
        if(ragController)
            ragController.TurnOnElementsIncludingChildren();
    }
}
