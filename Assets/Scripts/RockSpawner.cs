using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject rockPrefab;         // The rock prefab to spawn
    public float spawnInterval = 2f;    // Time between spawns
    public Vector2 spawnAreaSize = new Vector2(20f, 30f); // Width and height of the spawn area
    public float rockLifetime = 0.1f;      // Time before a rock is destroyed
    private bool playerInZone = false;   // Track if the player is in the zone

    private void Update()
    {
        // Start spawning when the player is in the zone
        if (playerInZone && !IsInvoking("SpawnRock"))
        {
            InvokeRepeating("SpawnRock", 0f, spawnInterval);
        }
        // Stop spawning when the player leaves the zone
        else if (!playerInZone && IsInvoking("SpawnRock"))
        {
            CancelInvoke("SpawnRock");
        }
    }

    private void SpawnRock()
    {
        // Randomize the spawn position within the spawn area
        float spawnX = Random.Range(transform.position.x - spawnAreaSize.x / 2, transform.position.x + spawnAreaSize.x / 2);
        float spawnY = transform.position.y;
        Vector2 spawnPosition = new Vector2(spawnX, spawnY);

        // Instantiate the rock prefab
        GameObject rock = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);

        // Destroy the rock after its lifetime
        Destroy(rock, rockLifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize the spawn area in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 1f));
    }
}

