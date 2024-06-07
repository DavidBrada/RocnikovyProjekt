using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float maxSpeed;
    
    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    [Space]
    public int extraJumpCount; // Počet skoků ve vzduchu
    [SerializeField] int remainingJumps;

    [Header("Ground Check")] 
    public bool grounded;
    public LayerMask groundLayer;
    public float playerHeight;
    
    [Space]
    public Transform orientation;

    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode dashKey = KeyCode.LeftShift;

    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;

    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public Rigidbody playerRb;
    [HideInInspector] public RaycastHit groundHit;
    [HideInInspector] public Vector3 flatVel;

    SlopeMove slopeMoveScript;
    Dashing dashScript;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        
        slopeMoveScript = GetComponent<SlopeMove>();
        dashScript = GetComponent<Dashing>();
    }
    
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, out groundHit, playerHeight * 0.5f + 0.3f, groundLayer);
        PlayerInput();
        SpeedControl();
        
        if (grounded)
        {
            remainingJumps = extraJumpCount;
            
            if (verticalInput == 0 && horizontalInput == 0 && !dashScript.isDashing)
            {
                playerRb.drag = 5;
            }
            else
            {
                playerRb.drag = 0;
            }
        }
        else
        {
            playerRb.drag = 0;
        }
    }

    void FixedUpdate()
    {
        MovePlayer();

        if (jumpCooldown > 0f)
        {
            jumpCooldown -= Time.deltaTime;
        }
        //Debug.Log(flatVel.magnitude.ToString("F2")); // Log rychlosti hráče (zobrazuje 2 desetinná místa)
    }

    void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && grounded && jumpCooldown <= 0f)
        {
            Jump();
        }
        else if (Input.GetKeyDown(jumpKey) && remainingJumps > 0)
        {
            Jump();
        }
    }

    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        if (slopeMoveScript.OnSlope())
        {
            playerRb.AddForce(slopeMoveScript.SlopeMoveDirection() * moveSpeed, ForceMode.Force);

            if (playerRb.velocity.y > 0)
            {
                playerRb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }
        else
        {
            playerRb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
        }

        if (slopeMoveScript.OnSlope() || dashScript.isDashing)
        {
            playerRb.useGravity = false;
        }
        else
        {
            playerRb.useGravity = true;
        }
    }


    void Jump()
    {
        remainingJumps--;
        jumpCooldown = 0.25f;
        
        playerRb.velocity = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z); // Vynulování vertikální rychlosti před skokem
        
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    void SpeedControl()
    {
        flatVel = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z); // Pohybová rychlost

        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = Vector3.ClampMagnitude(flatVel, maxSpeed);
            
            playerRb.velocity = new Vector3(limitedVel.x, playerRb.velocity.y, limitedVel.z);
        }
    }
}
