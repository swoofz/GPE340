using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Transform target;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private Vector3 desiredVelocity;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        navMeshAgent.SetDestination(target.position);
        Vector3 input = navMeshAgent.desiredVelocity;
        desiredVelocity = Vector3.MoveTowards(desiredVelocity, navMeshAgent.desiredVelocity, navMeshAgent.acceleration * Time.deltaTime);
        input = transform.InverseTransformDirection(desiredVelocity);
        animator.SetFloat("Horizontal", input.x);
        animator.SetFloat("Vertical", input.z);
    }

    private void OnAnimatorMove() {
        navMeshAgent.velocity = animator.velocity;
    }
}
