using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadLevel1()
    {
        Debug.Log("Button Clicked: Loading Level 1");
        SceneManager.LoadScene("level1"); // Replace "Level1" with the exact name of your scene
    }

    public void TestButton()
    {
        Debug.Log("Button Clicked!");
    }

}
