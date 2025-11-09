using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffect;
    public bool isAlive = true;

    public void TakeDamage(int damage)
    {
        if (isAlive == false) return;

        health -= damage;

        if (health <= 0)
        {
            isAlive = false;
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }
        }
        else
        {
            isAlive = true;
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
