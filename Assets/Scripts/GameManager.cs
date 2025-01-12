using UnityEngine;
using UnityEngine.SceneManagement; // For scene management
using TMPro; // For TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int totalKeys;           // Total keys in the scene
    private int keysCollected = 0;  // Keys collected by the player

    public TextMeshProUGUI keyCounterText; // Reference to the key counter UI text
    public GameObject gameOverPanel;      // Reference to the Game Over Panel
    public TextMeshProUGUI gameOverText;  // Optional: Game Over message text
    public string defaultGameOverMessage = "Game Over!";
    public Transform player;             // Reference to the player
    public float fallThreshold = -10f;  // Y-position below which the game ends

    public AudioSource audioSource;     // Reference to the AudioSource
    public AudioClip gameOverSound;     // Reference to the Game Over sound

    private bool isGameOver = false;    // Track if the game is over

    void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Find all keys in the scene and count them
        totalKeys = GameObject.FindGameObjectsWithTag("Key").Length;

        // Initialize the key counter UI
        UpdateKeyCounterUI();

        // Ensure the Game Over panel is initially inactive
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Check if the player has fallen below the fall threshold
        if (!isGameOver && player.position.y < fallThreshold)
        {
            TriggerGameOver("You fell!");
        }
    }

    // Called when a key is collected
    public void CollectKey()
    {
        keysCollected++;

        // Update the key counter UI
        UpdateKeyCounterUI();

        // Optional: Debug for tracking
        Debug.Log($"Keys Collected: {keysCollected}/{totalKeys}");
    }

    // Update the key counter UI
    private void UpdateKeyCounterUI()
    {
        if (keyCounterText != null)
        {
            keyCounterText.text = $"{keysCollected}";
        }
    }

    // Check if all keys are collected
    public bool AreAllKeysCollected()
    {
        return keysCollected >= totalKeys;
    }

    // Trigger Game Over
    public void TriggerGameOver(string message = null)
    {
        if (isGameOver) return;

        isGameOver = true;

        // Play Game Over sound
        if (audioSource != null && gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }

        // Display Game Over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Set Game Over message
        if (gameOverText != null)
        {
            gameOverText.text = message ?? defaultGameOverMessage;
        }

        // Pause the game
        Time.timeScale = 0f;
        Debug.Log("Game Over triggered.");
    }

    // Restart the game
    public void RestartGame()
    {
        // Reset game state
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }
}
