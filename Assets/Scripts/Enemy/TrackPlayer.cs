using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    public Transform player;
    void Update()
    {
        transform.LookAt(player);
    }
    
    
}
