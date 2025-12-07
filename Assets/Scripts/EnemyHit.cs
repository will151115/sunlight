using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public float damageAmount = 5f;
    public float damageCooldown = 1f;

    private float nextDamageTime = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthManager playerHealth = other.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                playerHealth.healthAmount -= damageAmount;
                playerHealth.isHit = true;
                nextDamageTime = Time.time + damageCooldown;
            }
        }
    }
}
