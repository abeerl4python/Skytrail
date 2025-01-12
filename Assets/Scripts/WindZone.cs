using UnityEngine;

public class WindZone : MonoBehaviour
{
    public Vector2 windForce = new Vector2(5f, 0f); // Direction and strength of the wind
    public AudioSource windAudioSource; // Reference to the wind AudioSource

    private void Start()
    {
        // Ensure the windAudioSource is assigned
        if (windAudioSource == null)
        {
            windAudioSource = GetComponent<AudioSource>();
        }

        // Initially, the wind sound should not play
        windAudioSource.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player entered the wind zone
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // Apply wind force once upon entering
                playerRb.AddForce(windForce, ForceMode2D.Force);
                Debug.Log("Wind applied to player: " + windForce);

                // Play wind sound
                windAudioSource.Play();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Continuously apply wind while the player is in the zone
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.AddForce(windForce, ForceMode2D.Force);
            }

            // Ensure wind sound is playing
            if (!windAudioSource.isPlaying)
            {
                windAudioSource.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Stop wind sound when the player exits the wind zone
        if (collision.CompareTag("Player"))
        {
            windAudioSource.Stop();
        }
    }
}
