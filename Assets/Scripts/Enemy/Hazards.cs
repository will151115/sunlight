using UnityEngine;

public class Hazards : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damageAmount = 5f;
    public float damageCooldown = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthManager playerHealth = other.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                playerHealth.healthAmount -= damageAmount;

                if (HazardCheckpoints.currentCheckpoint != null)
                {
                    // Move the player back to the last checkpoint
                    other.transform.position = HazardCheckpoints.currentCheckpoint.position;
                }
                else
                {
                    Debug.LogWarning("No checkpoint set yet!");
                }
            }
        }
    }
}
