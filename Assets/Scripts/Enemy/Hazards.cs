using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazards : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damageAmount = 5f;
    public float damageCooldown = 1f;

    public HazardCheckpoints hazardCheckpoints;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthManager playerHealth = other.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                playerHealth.healthAmount -= damageAmount;
                player.transform.position = HazardCheckpoints.currentCheckpoint.transform.position;
            }
        }
    }
}
