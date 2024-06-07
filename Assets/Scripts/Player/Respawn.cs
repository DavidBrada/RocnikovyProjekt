using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [HideInInspector] public Vector3 respawnPos;
    // Start is called before the first frame update
    void Start()
    {
        respawnPos = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ResetPlayer"))
        {
            transform.position = respawnPos;
        }
    }
}
