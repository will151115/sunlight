using UnityEngine;

public class BossAttackHitbox : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damageAmount = 5f;
    public float damageCooldown = 1f;

    private float nextDamageTime = 0f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Time.time >= nextDamageTime)
        {
            HealthManager playerHealth = other.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                playerHealth.healthAmount -= damageAmount;
                Debug.Log("Player took " + damageAmount + " damage from " + gameObject.name);
                nextDamageTime = Time.time + damageCooldown;
            }
        }
    }
}
