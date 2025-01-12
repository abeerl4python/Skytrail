using UnityEngine;
using UnityEngine.UI;

public class OxygenSystem : MonoBehaviour
{
    public Slider oxygenSlider; // Reference to the slider
    public Image sliderFill; // Reference to the Fill Image of the slider
    public float oxygenLevel = 100f; // Initial oxygen level
    public float oxygenDecreaseRate = 0.3f; // Rate of oxygen decrease
    private bool isGameOver = false; // To track if the game is over
    public Gradient gradient; // Gradient for color transition

    void Start()
    {
        // Set initial slider value and color
        oxygenSlider.maxValue = 100;
        oxygenSlider.value = oxygenLevel;
        sliderFill.color = gradient.Evaluate(1f); // Start with the max gradient color
    }

    void Update()
    {
        if (!isGameOver)
        {
            // Decrease oxygen over time
            oxygenLevel -= oxygenDecreaseRate * Time.deltaTime;
            oxygenLevel = Mathf.Clamp(oxygenLevel, 0, 100);

            // Update the slider
            oxygenSlider.value = oxygenLevel;

            // Update the fill color based on oxygen level (0 to 1 normalized)
            sliderFill.color = gradient.Evaluate(oxygenLevel / 100f);

            // Check for game over
            if (oxygenLevel <= 0)
            {
                GameOver();
            }
        }
    }

    public void DecreaseOxygen(float amount)
    {
        // Reduce the oxygen level
        oxygenLevel -= amount;
        oxygenLevel = Mathf.Clamp(oxygenLevel, 0, 100);

        // Update the slider
        oxygenSlider.value = oxygenLevel;

        // Update the fill color
        sliderFill.color = gradient.Evaluate(oxygenLevel / 100f);

        // Check if oxygen is depleted
        if (oxygenLevel <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isGameOver = true;

        Debug.Log("Game Over! Oxygen ran out.");

        // Trigger the game over in GameManager
        GameManager.instance.TriggerGameOver();
    }


}
