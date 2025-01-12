using UnityEngine;
using UnityEngine.SceneManagement; // For scene transitions

public class DoorController : MonoBehaviour
{
    public string level2; // Name of the next scene

    // This method is called by the Animation Event
    public void OnDoorOpenComplete()
    {
        // Load the next level
        SceneManager.LoadScene(level2);
    }
}
