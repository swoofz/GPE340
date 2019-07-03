using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : WeaponAgent {

    public Health Health { get; private set; }

    public Weapon[] defaultWeapons;                     // Array of weapons this Enemy can use
    [Header("Item Drop Settings")]
    [SerializeField, Range(0,1f)]
    private double itemDropChance = 1;  // Chance for an item to drop
    public WeightedObject[] itemDrops;  // Array of items that this enmy can drop

    private Player player;                      // Access to the only player in the game
    private NavMeshAgent navMeshAgent;          // Access to control Enemy action in using a NavMesh
    private Transform tf;                       // Get Enemies' Transform
    private Vector3 desiredVelocity;            // Store our deesired velocity
    private RagdollController ragController;    // Get our ragdoll controller component

    protected override void Awake() {
        // Initialize variables
        base.Awake();
        tf = GetComponent<Transform>();
        Health = GetComponent<Health>();
        ragController = GetComponent<RagdollController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        EquipWeapon(defaultWeapons[Random.Range(0, defaultWeapons.Length)]);
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (GameManager.Paused)
            return;

        FindPlayer();

        if (player) {
            Movement();
            Shoot();
        }
    }

    private void OnAnimatorMove() {
        // Set navMeshAgent velocity to our Root Motion for our animation
        navMeshAgent.velocity = Animator.velocity;
    }

    void Movement() {
        // Move towards our target/player
        navMeshAgent.SetDestination(player.transform.position);
        Vector3 input = navMeshAgent.desiredVelocity;
        desiredVelocity = Vector3.MoveTowards(desiredVelocity, navMeshAgent.desiredVelocity, navMeshAgent.acceleration * Time.deltaTime);
        input = transform.InverseTransformDirection(desiredVelocity);

        // Update animations
        Animator.SetFloat("Horizontal", input.x);
        Animator.SetFloat("Vertical", input.z);
    }

    void FindPlayer() {
        // Find the only player component in the game
        player = GameManager.Instance.playerPrefab;
        
        // No Player; Stop animations
        if (!player) {
            navMeshAgent.isStopped = true;
            Animator.SetFloat("Horizontal", 0f);
            Animator.SetFloat("Vertical", 0f);
            return;
        }
    }

    bool PlayerInSight() {
        // Find the angle in our forward looking direction to player position
        float angle = Vector3.Angle(player.transform.position - tf.position, transform.forward);

        // If angle is within the weapon attack angle
        if (angle < equippedWeapon.attackAngle)
            return true;    // return true

        return false;   // otherwise, return false
    }

    bool PlayerIsCloseEnough() {
        // Get the distance between our enemy and player
        float distanceBetween = Vector3.Distance(tf.position, player.transform.position);

        // Find out if player is within range to shoot
        if (distanceBetween < equippedWeapon.maxRange)
            return true;

        return false;
    }

    void Shoot() {
        // If player is in sight and close enough then shoot otherwise, dont shoot
        if (PlayerInSight() && PlayerIsCloseEnough()) equippedWeapon.PullTrigger();
        else equippedWeapon.ReleaseTrigger();
    }

    void OnDie() {
        // When die remove weapon, make enemy into ragdoll if has ragdoll controller and destory
        //      gameobject after some time
        Unequip();
        DropItem();
        if(ragController)
            ragController.TurnOnElementsIncludingChildren();
        Destroy(gameObject, 5f);
    }

    void DropItem() {
        if(Random.Range(0f, 1f) < itemDropChance)
            Instantiate(WeightedObject.Select(itemDrops), tf.position + Vector3.up, Quaternion.identity);
    }
}
