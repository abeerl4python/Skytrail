using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource

    private void Start()
    {
        if (audioSource != null)
        {
            audioSource.loop = true; // Ensure the music loops
            audioSource.Play();      // Start playing the music
        }
        else
        {
            Debug.LogWarning("AudioSource is not assigned!");
        }
    }

    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume); // Clamp volume between 0 and 1
    }
}
