using UnityEngine;
using UnityEngine.UI; // For UI components like the "You Win" screen

public class TreasureBox : MonoBehaviour
{
    private Animator animator; // Animator for the treasure box
    private bool playerNearby = false; // To track if the player is near the treasure box
    public GameObject youWinPanel; // UI panel for "You Win" screen
    public AudioClip victorySound; // Sound clip for victory
    private AudioSource audioSource; // AudioSource for playing sounds

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (youWinPanel != null)
        {
            youWinPanel.SetActive(false); // Ensure the "You Win" panel is hidden at the start
        }

        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource found on the TreasureBox. Please add one.");
        }
    }

    void Update()
    {
        // Check if the player presses "E" near the treasure box AND all keys are collected
        if (playerNearby && Input.GetKeyDown(KeyCode.E) && GameManager.instance.AreAllKeysCollected())
        {
            OpenTreasureBox();
        }
    }

    private void OpenTreasureBox()
    {
        animator.SetTrigger("Open"); // Trigger the "Open" animation
        Debug.Log("Treasure Box Opened!");

        // Play the victory sound
        if (audioSource != null && victorySound != null)
        {
            audioSource.PlayOneShot(victorySound);
        }

        // Show the "You Win" screen after a short delay
        Invoke(nameof(ShowWinScreen), 1.5f);
    }

    private void ShowWinScreen()
    {
        if (youWinPanel != null)
        {
            youWinPanel.SetActive(true);
            Time.timeScale = 0; // Pause the game
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
