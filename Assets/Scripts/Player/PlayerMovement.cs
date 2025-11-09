using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer playerSprite;


    private bool isGhost;

    public bool playerCanTakeDamage = true;

    [SerializeField] private float ghostDuration = 3f;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            anim.SetTrigger("jump");
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();

        if (Input.GetKeyDown(KeyCode.G) && !isGhost)
        {
            StartCoroutine(GhostMode());
        }

        if (Mathf.Abs(horizontal) > 0.1f)
        {
            //Debug.Log($"Vel: {rb.velocity}, Simulated: {rb.simulated}");
        }

    }

    private void FixedUpdate()
    {
        /*
        if (rb.velocity.x == 0 && horizontal != 0)
        {
            Debug.Log("Velocity was forced to zero at time " + Time.time);
            Debug.Log("Stack trace: " + System.Environment.StackTrace);
        }
        */

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }


    private IEnumerator GhostMode()
    {
        isGhost = true;
        playerSprite.color = new Color(1f, 1f, 1f, 0.5f);

        playerCanTakeDamage = false;

        yield return new WaitForSeconds(ghostDuration);

        playerCanTakeDamage = true;
        playerSprite.color = new Color(1f, 1f, 1f, 1f);
        isGhost = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collided with: " + collision.collider.name);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("HazardCheckpoint"))
        {

        }
    }

}
