using UnityEngine;

public class WindZoneEffect : MonoBehaviour
{
    public ParticleSystem windEffect; // Reference to the Particle System

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Check if the player enters
        {
            windEffect.Play(); // Start the wind particles
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Check if the player exits
        {
            windEffect.Stop(); // Stop the wind particles
        }
    }
}
