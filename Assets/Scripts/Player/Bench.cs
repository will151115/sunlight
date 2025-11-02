using UnityEngine;

public class Bench : MonoBehaviour
{
    private bool isTouching;
    public Animator anim;
    public HealthManager HealthManager;
    public float maxHealth = 100f;

    void Update()
    {
        if (isTouching && Input.GetKeyDown(KeyCode.E))
        {
            // Heal the player
            HealthManager.healthAmount = maxHealth;
            anim.SetBool("Resting", true);

            // Save this bench position globally for respawn
            GameManager.instance.respawnPosition = transform.position;

            Debug.Log("Respawn point set to: " + GameManager.instance.respawnPosition);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTouching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTouching = false;
        }
    }
}
