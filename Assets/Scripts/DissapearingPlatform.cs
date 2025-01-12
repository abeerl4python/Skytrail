using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    public float disappearDelay = 0.5f; // Time before the platform disappears
    public float reappearDelay = 3f;    // Time before the platform reappears

    private Collider2D platformCollider;
    private SpriteRenderer platformRenderer;

    void Start()
    {
        platformCollider = GetComponent<Collider2D>();
        platformRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player stepped on the platform
        if (collision.gameObject.CompareTag("Player"))
        {
            // Start the disappear and reappear sequence
            Invoke(nameof(Disappear), disappearDelay);
            Invoke(nameof(Reappear), disappearDelay + reappearDelay);
        }
    }

    void Disappear()
    {
        // Disable collider and make platform invisible
        platformCollider.enabled = false;
        platformRenderer.enabled = false;
    }

    void Reappear()
    {
        // Re-enable collider and make platform visible
        platformCollider.enabled = true;
        platformRenderer.enabled = true;
    }
}
