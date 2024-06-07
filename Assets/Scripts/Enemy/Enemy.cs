using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Enemy nedávají dmg, enemy projektily se nemažou

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    public float shootForce;
    public float timeBetweenAttacks;
    public Transform attackPoint;
    bool playerInRange;
    public float attackRange;
    bool alreadyAttacked;
    
    public float health;
    Gun gunScript;
    Progression progressScript;
    public Transform player;
    public LayerMask playerMask;

    void Start()
    {
        gunScript = FindObjectOfType<Gun>();
        player = GameObject.Find("PlayerCam").transform;
        progressScript = FindObjectOfType<Progression>();
    }
    
    void Update()
    {
        transform.LookAt(player);

        playerInRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

        if (playerInRange)
        {
            AttackPlayer();
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Die();
            progressScript.eliminationCount++;
        }
    }

    void Die()
    {
        Destroy(transform.parent.gameObject);
        gunScript.eliminations++;

        gunScript.bulletsLeft = gunScript.ammoCapacity;
    }

    void AttackPlayer()
    {
        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, attackPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.AddForce(transform.forward * 32f * shootForce, ForceMode.Impulse);
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
