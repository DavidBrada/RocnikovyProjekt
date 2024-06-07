using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
        Transform playerForwardTransform;

    [Header("References")]
    public Transform orientation;
    public Transform playerCamera;
    Rigidbody rb;
    PlayerMove pm; // PlayerMovement script
    SlopeMove slopeMoveScript;

    [Header("Dashing")]
    public float dashForce;
    public float dashDuration;
    public float maxDashSpeed;
    public float speedSmooth;
    public bool isDashing;

    [Header("Cooldown")]
    public float dashCooldown;
    private float dashCooldownTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.LeftShift;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMove>();
        slopeMoveScript = GetComponent<SlopeMove>();
    }

    void Update()
    {
        if(Input.GetKeyDown(dashKey) && !slopeMoveScript.OnSlope())
        {
            Dash();
        }

        if(pm.grounded && dashCooldownTimer == dashCooldown)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
        else if(dashCooldownTimer > 0 && dashCooldownTimer != dashCooldown)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (pm.maxSpeed > 10f)
        {
            pm.maxSpeed -= speedSmooth * Time.deltaTime;
        }
        else if (pm.maxSpeed < 10f)
        {
            pm.maxSpeed = 10f;
        }
        
    }

    void Dash()
    {
        if(dashCooldownTimer > 0)
        {
            return;
        }
        else
        {
            dashCooldownTimer = dashCooldown;
        }
        
        isDashing = true;
        pm.maxSpeed = maxDashSpeed;
        
        FindObjectOfType<AudioManager>().Play("PlayerDash");
        
        playerForwardTransform = orientation;

        Vector3 direction = GetDirection(playerForwardTransform);
        Vector3 forceToApply = direction * dashForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(forceToApply, ForceMode.Impulse);

        Invoke(nameof(ResetDash), dashDuration);
    }

    private void ResetDash()
    {
        isDashing = false;
    }

    private Vector3 GetDirection(Transform forward)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3();
        if(verticalInput != 0 || horizontalInput != 0)
        {
            direction = playerForwardTransform.forward * verticalInput + playerForwardTransform.right * horizontalInput;
        }
        else
        {
            direction = playerForwardTransform.forward;
        }
        

        return direction.normalized;
    }
}