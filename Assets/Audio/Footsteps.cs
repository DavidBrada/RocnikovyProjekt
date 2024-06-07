using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    PlayerMove playerMoveScript;
    AudioSource footsteps;
    void Start()
    {
        playerMoveScript = GetComponent<PlayerMove>();
        footsteps = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMoveScript.moveDirection != Vector3.zero && playerMoveScript.grounded)
        {
            footsteps.mute = false;
        }
        else
        {
            footsteps.mute = true;
        }
    }
}
