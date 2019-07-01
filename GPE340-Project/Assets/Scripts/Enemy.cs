using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : WeaponAgent {

    public Health Health { get; private set; }

    public Player player;
    public Weapon[] defaultWeapons;

    private NavMeshAgent navMeshAgent;
    private Transform tf;
    private Vector3 desiredVelocity;
    private RagdollController ragController;

    protected override void Awake() {
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
        FindPlayer();
        Movement();
        Shoot();
    }

    private void OnAnimatorMove() {
        navMeshAgent.velocity = Animator.velocity;
    }

    void Movement() {
        navMeshAgent.SetDestination(player.transform.position);
        Vector3 input = navMeshAgent.desiredVelocity;
        desiredVelocity = Vector3.MoveTowards(desiredVelocity, navMeshAgent.desiredVelocity, navMeshAgent.acceleration * Time.deltaTime);
        input = transform.InverseTransformDirection(desiredVelocity);
        Animator.SetFloat("Horizontal", input.x);
        Animator.SetFloat("Vertical", input.z);
    }

    void FindPlayer() {
        player = FindObjectOfType<Player>();
        if (!player) {
            navMeshAgent.isStopped = true;
            Animator.SetFloat("Horizontal", 0f);
            Animator.SetFloat("Vertical", 0f);
            return;
        }
    }

    bool PlayerInSight() {
        float angle = Vector3.Angle(tf.position, player.transform.position);

        if (angle < equippedWeapon.attackAngle)
            return true;

        return false;
    }

    bool PlayerIsCloseEnough() {
        float distanceBetween = Vector3.Distance(tf.position, player.transform.position);

        if (distanceBetween < equippedWeapon.maxRange)
            return true;

        return false;
    }

    void Shoot() {
        if (PlayerInSight() && PlayerIsCloseEnough()) equippedWeapon.PullTrigger();
        else equippedWeapon.ReleaseTrigger();
    }

    void OnDie() {
        Unequip();
        if(ragController)
            ragController.TurnOnElementsIncludingChildren();
        Destroy(gameObject, 5f);
    }
}
