using UnityEngine;

public class RockDamage : MonoBehaviour
{
    public int damageAmount = 3; // Amount of oxygen to decrease

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.collider.name);

        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Rock hit the player!");

            // Access the OxygenSystem on the player
            OxygenSystem oxygenSystem = collision.collider.GetComponent<OxygenSystem>();
            if (oxygenSystem != null)
            {
                Debug.Log("OxygenSystem found on the player, reducing oxygen.");
                oxygenSystem.DecreaseOxygen(damageAmount); // Reduce oxygen
            }
            else
            {
                Debug.LogWarning("OxygenSystem not found on the player.");
            }

            // Destroy the rock after causing damage
            Destroy(gameObject);
        }
    }
}
