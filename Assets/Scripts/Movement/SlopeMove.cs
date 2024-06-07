using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;
using Vector3 = UnityEngine.Vector3;

public class SlopeMove : MonoBehaviour
{
    // Zjistit, jestli je hráč na rampě, (vypnout gravitaci)
    PlayerMove playerMoveScript;

    public float maxSlopeAngle;
    RaycastHit slopeHit;
    
    void Start()
    {
        playerMoveScript = GetComponent<PlayerMove>();
    }
    
    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerMoveScript.playerHeight * 0.5f + 0.4f, playerMoveScript.groundLayer))
        {
            float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return slopeAngle < maxSlopeAngle && slopeAngle != 0;
        }
        
        return false;
    }

    public Vector3 SlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(playerMoveScript.moveDirection, slopeHit.normal).normalized;
    }
}
