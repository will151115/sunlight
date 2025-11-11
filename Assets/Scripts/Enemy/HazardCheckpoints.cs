using UnityEngine;

public class HazardCheckpoints : MonoBehaviour
{
    // Static means it’s shared — every hazard can access it
    public static Transform currentCheckpoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Set this checkpoint as the current one
            currentCheckpoint = transform;
            Debug.Log("Checkpoint set to: " + gameObject.name);
        }
    }
}
