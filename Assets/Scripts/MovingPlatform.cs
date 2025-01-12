using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private bool playerOnPlatform = false;  // Tracks if the player is on the platform
    private Transform player;  // Reference to the player's transform

    void Update()
    {
        if (playerOnPlatform && player != null)
        {
            // Make the platform follow the player's X position while keeping its Y constant
            transform.position = new Vector2(player.position.x, transform.position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player has stepped on the platform
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;  // Player is now on the platform
            player = collision.transform;  // Store the player's transform
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the player has left the platform
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = false;  // Player is no longer on the platform
            player = null;  // Clear the player reference
        }
    }
}


