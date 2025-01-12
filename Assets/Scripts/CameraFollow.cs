using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player's Transform
    public float smoothSpeed = 0.125f;  // Initial smooth speed
    public Vector3 offset;  // Offset from the player
    public float levelHeight = 50f;  // Approximate height of the level
    public float speedChangeThreshold = 0.5f;  // Percentage of level height to trigger speed change
    public float fasterSmoothSpeed = 0.2f;  // Speed after reaching the threshold

    private float originalSmoothSpeed;

    void Start()
    {
        originalSmoothSpeed = smoothSpeed;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Calculate the target camera position
            Vector3 targetPosition = new Vector3(transform.position.x, player.position.y + offset.y, transform.position.z);

            // Adjust smooth speed based on player's height
            if (player.position.y > levelHeight * speedChangeThreshold)
            {
                smoothSpeed = Mathf.Lerp(smoothSpeed, fasterSmoothSpeed, Time.deltaTime);
            }
            else
            {
                smoothSpeed = Mathf.Lerp(smoothSpeed, originalSmoothSpeed, Time.deltaTime);
            }

            // Smoothly move the camera to the target position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
