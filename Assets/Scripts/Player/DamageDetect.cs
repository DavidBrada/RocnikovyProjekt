using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetect : MonoBehaviour
{
    PlayerHP playerHp;
    void Start()
    {
        playerHp = GetComponent<PlayerHP>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            playerHp.TakeDamage(20);
        }
    }
}
