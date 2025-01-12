using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 2f; // Speed of scrolling

    void Update()
    {
        // Move the background upward over time
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);

        // Optional: Loop the background (requires multiple background images)
        if (transform.position.y >= Screen.height) // Adjust based on your setup
        {
            transform.position -= new Vector3(0, Screen.height * 2, 0); // Reset position for looping
        }
    }
}
