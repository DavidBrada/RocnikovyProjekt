using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerHP : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public float enemyDamageCoolDown;
    public float currentEnemyDamageCooldown;
    Respawn respawnScript;
    

    void Start()
    {
        currentHP = maxHP;
        respawnScript = GetComponent<Respawn>();
    }

    void Update()
    {
        if (currentEnemyDamageCooldown > 0)
        {
            currentEnemyDamageCooldown -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHP -= damageAmount;

        if(currentHP <= 0)
        {
            Die();
            currentHP = maxHP;
        }
    }

    void OnCollisionStay(Collision collisiom)
    {
        if (collisiom.gameObject.CompareTag("Enemy") && currentEnemyDamageCooldown <= 0)
        {
            TakeDamage(10);
            currentEnemyDamageCooldown = enemyDamageCoolDown;
        }
    }

    void Die()
    {
        transform.position = respawnScript.respawnPos;
    }
}
