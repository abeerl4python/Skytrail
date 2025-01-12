using UnityEngine;
using TMPro; // For TextMeshPro

public class KeyCounterUI : MonoBehaviour
{
    public TextMeshProUGUI keyCounterText; // Reference to the UI Text component
    public int totalKeys = 0;             // Number of keys collected

    // Call this method to update the key counter
    public void UpdateKeyCounter(int keysCollected)
    {
        totalKeys = keysCollected;
        keyCounterText.text = totalKeys.ToString();
    }
}
