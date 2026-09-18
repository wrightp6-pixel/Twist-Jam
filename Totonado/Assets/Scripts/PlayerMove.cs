using UnityEngine;
using UnityEngine.InputSystem;

// This class is heavily inlfuenced by an duses soem code from iHeartGameDev on YouTube

public class PlayerMove : MonoBehaviour
{
    // ===================== Instance Properties ===================== 
    // Reference to input actions from new input system
    private MyControls myControls;

    CharacterController charCon;

    private Vector2 playerMovementFlat;
    private Vector3 playerMovementTotal;
    [SerializeField] private int moveSpeed;
    [SerializeField] private float rotationRate;
    private bool isPressingMove;

    [SerializeField] private float gravity;
    [SerializeField] private float groundedGravity;

    // Jump Variables
    [SerializeField] private float initialJumpVelocity;
    [SerializeField] private float maxJumpHeight = 1.0f;
    [SerializeField] private float maxJumpTime; // seconds
    [SerializeField] private float fallMultipler; // Chnages fall time
    private bool isPressingJump = false;
    private bool isJumping = false;





    // Built in method that is run very first in the object's lifecycle, including before start
    private void Awake()
    {
        // Set refernce to input action, character controller
        myControls = new MyControls();
        charCon = GetComponent<CharacterController>();

        // callbacks
        myControls.PlayerMoveControls.Jump.started += onJump;
        myControls.PlayerMoveControls.Jump.canceled += onJump;

        // =========================== Important NOte ===========================
        // Watch GDC math for game gameprogrammers, building a better jump video to learn more about how thsi works
        // Set up jump variables
        // The time time to apex (the piint where ypu reach max jump height) occurs halfway through jump
        float timeToApex = maxJumpTime / 4;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
        isJumping = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPressingMove = false;
        myControls.PlayerMoveControls.Enable();
        playerMovementFlat = new Vector2(0, 0);
        playerMovementTotal = new Vector3(0, 0, 0);

        
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        if (isPressingMove)
        {
            handleRotation();
        }
        // Because grounded movement is absed on where player was last frame and gravity effects taht check, it
        // must go after movement
        handleGravity();
        // Jump must go after handleGravity because, in handleGravity, we set y velocity = 0 if on the ground, which would wipe out our change to y velcoity in handleJump
        handleJump();


    }

    // ===================== Methods =====================

    private void movePlayer()
    {
        // Update movement value based on player inputs
        playerMovementFlat = myControls.PlayerMoveControls.Move.ReadValue<Vector2>();
        playerMovementTotal.x = playerMovementFlat.x * moveSpeed;
        playerMovementTotal.z = playerMovementFlat.y * moveSpeed;

        if(playerMovementFlat.x == 0 && playerMovementFlat.y == 0)
        {
            isPressingMove = false;
        } else
        {
            isPressingMove = true;
        }

        // Move
        charCon.Move(playerMovementTotal * Time.deltaTime);
    }

    // Make the player rotate in the direction thay are moving towards
    // This code is based off tutorial by iHeartGameDev on YouTube
    private void handleRotation()
    {
        // Get the direction to turn towards to be the direction the player is moving in
        Vector3 positionToLookAt = new Vector3(playerMovementTotal.x, 0.0f, playerMovementTotal.z);

        Quaternion currRotation = transform.rotation;

       
        // Move player character rotation to be looking in direction 
        Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
        // Uses spherical inetrpolation to rotate between current rotation and target rotation
        transform.rotation = Quaternion.Slerp(currRotation, targetRotation, rotationRate *Time.deltaTime);
        

    }

    private void handleGravity()
    {
        // Apply fall mutliplier if player has started falling or let go of jump button
        bool isFalling = playerMovementTotal.y <= 0 || !isPressingJump;
        // When the player is grounded, still need to apply a downward force. This is becasue
        // Unity's character controller collision detection only works if the player is moving in
        // the direction of the colliding object (the ground) and otherwise won't recognize it
        if(charCon.isGrounded)
        {
            playerMovementTotal.y = groundedGravity * Time.deltaTime;
        } 
        else if(isFalling)
        {
            // Faster falling speed
            float previousYVelocity = playerMovementTotal.y;
            float newYVelocity = playerMovementTotal.y + (gravity * fallMultipler * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;

            playerMovementTotal.y = nextYVelocity;
        }
        else
        {
            // Average old and new velocities to stop issue with Euler jump integration (the type here), which has slight differences
            // at different framerates
            float previousYVelocity = playerMovementTotal.y;
            float newYVelocity = playerMovementTotal.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;

            playerMovementTotal.y = nextYVelocity;
        }
    }

    public void handleJump()
    {
        
        // Check if player can jump
        if (!isJumping && charCon.isGrounded && isPressingJump)
        {
            Debug.Log("Jump");
            isJumping = true;
            // Set the intial velocity of jump (initial velocity is always the point when velocity is greatest)
            playerMovementTotal.y = initialJumpVelocity;
        } else if (charCon.isGrounded && !isPressingJump && isJumping)
        {
            isJumping = false;
        }

        
    }

    private void onJump (InputAction.CallbackContext context)
    {
        // See whether the player has pressed the jump button
        isPressingJump = context.ReadValueAsButton();
        Debug.Log(isPressingJump);
    }

}
