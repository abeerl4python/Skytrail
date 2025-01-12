using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    private bool playerNearby = false; // Track if the player is near the door

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the player presses "E" near the door AND all keys are collected
        if (playerNearby && Input.GetKeyDown(KeyCode.E) && GameManager.instance.AreAllKeysCollected())
        {
            animator.SetTrigger("Open");
            Debug.Log("Door opening!");
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
