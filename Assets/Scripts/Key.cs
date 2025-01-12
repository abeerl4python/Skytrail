using UnityEngine;

public class Key : MonoBehaviour
{
    private bool isCollected = false; // Prevent multiple triggers
    public AudioClip collectSound;   // Audio clip for the key collection sound
    public float audioVolume = 1.0f; // Volume for the audio clip

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true; // Mark as collected
            Debug.Log("Key collected: " + gameObject.name);

            // Play the audio clip at the key's position
            if (collectSound)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position, audioVolume);
            }

            // Notify the GameManager
            GameManager.instance.CollectKey();

            // Destroy the key immediately
            Destroy(gameObject);
        }
    }
}
