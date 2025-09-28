using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("Scene attack objects (placed in world, not children of boss)")]
    public GameObject[] attacks; // Assign RayAttack, CircleAttack, SquareAttack directly from scene

    [Header("References")]
    public Enemy bossHealth;    // Boss's health script
    public Animator anim;

    [Header("Attack Settings")]
    public float attackCooldown = 2f;   // time between attacks
    public float attackDuration = 5f;   // how long the attack stays active

    [Header("Teleport Settings")]
    public Transform[] teleportPoints; // Assign fixed teleport points in scene
    public float teleportCooldown = 5f;

    private bool isAlive = true;
    private bool playerInRange = false;

    private bool canTeleport = true;
    private bool canAttack = true;
    private bool stagger;

    private void Start()
    {
        // Disable all attacks at start
        foreach (GameObject atk in attacks)
        {
            if (atk != null) atk.SetActive(false);
        }

        if (canAttack == true)
        {
            StartCoroutine(AttackLoop());
        }

        StartCoroutine(TeleportLoop());

        // StartCoroutine(Stagger());
    }

    public void Update()
    {
    
    }

    private IEnumerator AttackLoop()
    {
        while (isAlive)
        {
            if (canAttack)
            {
                yield return new WaitForSeconds(attackCooldown);

                if (bossHealth.health <= 0f)
                {
                    isAlive = false;
                    yield break;
                }

                if (playerInRange) // only attack when player is in range
                {
                    // pick random attack
                    GameObject chosen = attacks[Random.Range(0, attacks.Length)];
                    anim.SetBool("attackActive", true);
                    yield return new WaitForSeconds(0.5f);

                    // activate attack (keeps its original world position)
                    chosen.SetActive(true);
                    Debug.Log("Boss activated: " + chosen.name);

                    // wait while attack is active
                    yield return new WaitForSeconds(attackDuration);

                    // deactivate attack
                    chosen.SetActive(false);
                    anim.SetBool("attackActive", false);
                }

            }
        }
    }

    private IEnumerator TeleportLoop()
    {
        while (isAlive)
        {
            if (canTeleport)
            {
                yield return new WaitForSeconds(teleportCooldown);

                if (teleportPoints.Length > 0)
                {

                    Transform chosenPoint = teleportPoints[Random.Range(0, teleportPoints.Length)];
                    transform.position = chosenPoint.position;
                    Debug.Log("Boss teleported to: " + chosenPoint.name);
                }
            }

        }
    }

    // Detect if player enters boss range
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered boss range.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player left boss range.");
        }
    }

    private IEnumerator Stagger()
    {
        if (bossHealth.health == 200)
        {
            canAttack = false;
            canTeleport = false;
            anim.SetTrigger("Stagger");
            yield return new WaitForSeconds(3f);
        }

        if (bossHealth.health == 100)
        {
            canAttack = false;
            canTeleport = false;
            anim.SetTrigger("Stagger");
            yield return new WaitForSeconds(3f);
        }

        if (bossHealth.health == 50)
        {
            canAttack = false;
            canTeleport = false;
            anim.SetTrigger("Stagger");
            yield return new WaitForSeconds(3f);
        }
    }
    
}
