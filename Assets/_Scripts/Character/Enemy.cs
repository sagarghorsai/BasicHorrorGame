using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float health = 100f;
    [Header("Detection Ranges")]
    [SerializeField] private float sightRange = 20f;
    [SerializeField] private float attackRange = 5f;
    private bool playerInSightRange;
    private bool playerInAttackRange;
    public bool isAttacking;
    [Header("References")]
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundLayer, playerLayer;
    public Animator anim;

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
        if (!playerInSightRange && !playerInAttackRange) Patroling();
    }

    private void Patroling()
    {
        if (!agent.hasPath || agent.remainingDistance < 0.5f)
        {
            Vector3 randomDirection = Random.insideUnitSphere * (sightRange * 2);
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, sightRange * 2, NavMesh.AllAreas);
            Vector3 finalPosition = hit.position;
            agent.SetDestination(finalPosition);

            // Trigger walking animation
            anim.SetBool("IsWalking", true);
        }
        else
        {
            // Ensure walking animation continues while moving
            anim.SetBool("IsWalking", true);
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        // You might want to set a different animation for chasing
        anim.SetBool("IsRunning", true);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (!isAttacking)
        {
            isAttacking = true;
            // Stop walking animation and trigger attack animation
            anim.SetBool("IsWalking", false);
            anim.SetTrigger("Attack");
            Invoke(nameof(ResetAttack), 1.5f);
        }
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}