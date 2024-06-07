using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportTo;

    void OnTriggerEnter(Collider other)
    {
        other.transform.position = teleportTo.transform.position;
    }
}
