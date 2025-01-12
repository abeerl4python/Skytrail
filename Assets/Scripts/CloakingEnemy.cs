using UnityEngine;

public class CloakingEnemy : MonoBehaviour
{
    public Transform player;                  // Reference to the player's transform
    public float speed = 2f;                  // Movement speed of the enemy
    public float followRange = 15f;           // Range within which the enemy follows the player
    public float visibilityToggleInterval = 5f; // Time interval for visibility toggling
    public float knockbackForce = 20f;        // Force to push the player downwards
    public float knockbackCooldown = 3f;      // Cooldown time between knockbacks
    public AudioClip alertSound;              // Sound played when the player enters range
    public AudioClip knockbackSound;          // Sound played when the player is knocked back
    public float platformDisappearDuration = 2f; // Duration for which the platform disappears

    private SpriteRenderer spriteRenderer;
    private Collider2D enemyCollider;         // Collider for toggling interactions
    private AudioSource audioSource;          // AudioSource for playing sounds
    private bool isVisible = true;            // Tracks whether the enemy is visible
    private bool canKnockback = true;         // Tracks whether the enemy can knock the player back

    void Start()
    {
        // Cache the sprite renderer, collider, and AudioSource
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();

        // Start toggling visibility
        InvokeRepeating(nameof(ToggleVisibility), visibilityToggleInterval, visibilityToggleInterval);
    }

    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (player == null) return;

        // Calculate the distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Only follow the player if they are within range and the enemy is visible
        if (distanceToPlayer <= followRange && isVisible)
        {
            // Move toward the player
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);

            // Play the alert sound if the player enters range and it hasn't been played yet
            if (!audioSource.isPlaying && alertSound != null)
            {
                audioSource.PlayOneShot(alertSound);
            }
        }
    }

    private void ToggleVisibility()
    {
        isVisible = !isVisible;
        spriteRenderer.enabled = isVisible; // Toggle sprite visibility
        enemyCollider.enabled = isVisible; // Toggle collider interaction
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canKnockback)
        {
            Debug.Log("Enemy knocked the player!");

            Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
            Collider2D playerCollider = other.GetComponent<Collider2D>();

            if (playerRigidbody != null && playerCollider != null)
            {
                // Apply downward velocity directly
                playerRigidbody.linearVelocity = new Vector2(playerRigidbody.linearVelocity.x, -knockbackForce);

                // Debugging the Raycast
                Debug.DrawRay(player.position, Vector2.down * 2f, Color.red, 2f);

                // Make the platform below the player disappear
                RaycastHit2D hit = Physics2D.Raycast(player.position, Vector2.down, 2f);
                if (hit.collider != null)
                {
                    Debug.Log($"Raycast hit: {hit.collider.gameObject.name}");

                    if (hit.collider.CompareTag("Platform"))
                    {
                        GameObject platform = hit.collider.gameObject;
                        Debug.Log("Disabling platform: " + platform.name);
                        StartCoroutine(DisablePlatformTemporarily(platform));
                    }
                }
                else
                {
                    Debug.Log("Raycast did not hit anything.");
                }

                // Play the knockback sound
                if (knockbackSound != null)
                {
                    audioSource.PlayOneShot(knockbackSound);
                }

                // Start knockback cooldown
                canKnockback = false;
                Invoke(nameof(ResetKnockback), knockbackCooldown);
            }
        }
    }


    private System.Collections.IEnumerator DisablePlatformTemporarily(GameObject platform)
    {
        // Disable the platform
        platform.SetActive(false);

        // Wait for the specified duration
        yield return new WaitForSeconds(platformDisappearDuration);

        // Re-enable the platform
        platform.SetActive(true);
    }

    private void ResetKnockback()
    {
        canKnockback = true;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the follow range in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followRange);
    }
}
