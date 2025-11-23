using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float raycastDistance = 0.5f;
    public LayerMask groundLayer;
    private int direction = 1;

    void Update()
    {
        Move();
        CheckForCliff();
    }

    void Move()
    {
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);
    }

    void CheckForCliff()
    {
        Vector2 raycastOrigin = transform.position + new Vector3(direction * raycastDistance, -0.5f, 0);
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down, 1.0f, groundLayer);

        if (hit.collider == null)
        {
            TurnAround();
        }
    }

    void TurnAround()
    {
        direction *= -1;
        transform.localScale = new Vector3(direction, 1, 1); // Flip the sprite
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bounds"))
        {
            TurnAround();
        }
    }
}
