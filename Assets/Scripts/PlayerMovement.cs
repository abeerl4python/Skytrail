using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float baseMoveSpeed = 6f;    // Base movement speed
    public float maxMoveSpeed = 13f;   // Maximum movement speed
    public float baseJumpForce = 11f;  // Base jump force
    public float maxJumpForce = 18f;   // Maximum jump force
    public Transform groundCheck;      // Ground check transform
    public float groundCheckRadius = 0.2f;  // Radius for ground check
    public LayerMask groundLayer;      // Layer to detect ground
    public Animator animator;          // Animator for animations
    public float levelHeight = 100f;    // Approximate height of the level

    private Rigidbody2D rb;            // Rigidbody2D component
    private bool isGrounded;           // Whether the player is on the ground
    private bool facingRight = true;   // Track the character's facing direction

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Gradually increase speed and jump force based on vertical position
        float progress = Mathf.Clamp01(transform.position.y / levelHeight);
        float moveSpeed = Mathf.Lerp(baseMoveSpeed, maxMoveSpeed, progress);
        float jumpForce = Mathf.Lerp(baseJumpForce, maxJumpForce, progress);

        // Get horizontal input
        float moveInput = Input.GetAxis("Horizontal");

        // Set horizontal velocity
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Update animator speed parameter
        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        // Flip the character sprite if necessary
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

        // Jump logic
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Jump by modifying vertical velocity
            animator.SetTrigger("TakeOff"); // Trigger the takeoff animation
        }
    }

    // Flip the character sprite
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // Flip the x-axis scale
        transform.localScale = localScale;
    }
}
