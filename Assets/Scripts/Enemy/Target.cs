using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50;
    Gun gunScript;
    Progression progressScript;

    void Start()
    {
        gunScript = FindObjectOfType<Gun>();
        progressScript = FindObjectOfType<Progression>();
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Die();
            progressScript.targetCount++;
        }
    }

    void Die()
    {
        Destroy(gameObject);
        gunScript.targetsHit++;

        gunScript.bulletsLeft = gunScript.ammoCapacity;
    }
}
