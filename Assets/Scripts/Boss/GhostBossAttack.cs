using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("Scene attack objects (placed in world, not children of boss)")]
    public GameObject[] attacks; // Assign RayAttack, CircleAttack, SquareAttack directly from scene

    [Header("References")]
    public Enemy bossHealth;    // Boss's health script
    public Animator anim;
    // public Animator doorAnim;

    [Header("Attack Settings")]
    public float attackCooldown = 2f;   // time between attacks
    public float attackDuration = 5f;   // how long the attack stays active

    [Header("Teleport Settings")]
    public Transform[] teleportPoints; // Assign fixed teleport points in scene
    public float teleportCooldown = 5f;

    private bool isAlive = true;
    private bool playerInRange = false;

    [Header("Stagger settings")]
    private bool canTeleport = true;
    private bool canAttack = true;
    private bool hasStaggered75 = false;
    private bool hasStaggered50 = false;
    private bool hasStaggered25 = false;

    private void Start()
    {
        // Disable all attacks at start
        foreach (GameObject atk in attacks)
        {
            if (atk != null) atk.SetActive(false);
        }

        
        StartCoroutine(AttackLoop());

        StartCoroutine(TeleportLoop());
    }

    private void Update()
    {
        // Stagger triggers at thresholds (75%, 50%, 25%)
        if (isAlive && bossHealth != null && bossHealth.isAlive)
        {
            if (!hasStaggered75 && bossHealth.health <= 75)
            {
                StartCoroutine(Stagger());
                hasStaggered75 = true;
            }
            else if (!hasStaggered50 && bossHealth.health <= 50)
            {
                StartCoroutine(Stagger());
                hasStaggered50 = true;
            }
            else if (!hasStaggered25 && bossHealth.health <= 25)
            {
                StartCoroutine(Stagger());
                hasStaggered25 = true;
            }
        }
    }

    private IEnumerator AttackLoop()
    {
        while (isAlive)
        {
            if (canAttack)
            {
                yield return new WaitForSeconds(attackCooldown);

                if (!bossHealth.isAlive)
                {
                    yield return StartCoroutine(Death());
                    yield break;
                }

                if (playerInRange)
                {
                    anim.SetBool("attackActive", true);
                    yield return new WaitForSeconds(0.5f);

                    // pick random attack
                    GameObject chosen = attacks[Random.Range(0, attacks.Length)];

                    // detach to avoid following the boss
                    chosen.transform.SetParent(null, true);

                    // activate the attack
                    chosen.SetActive(true);
                    Debug.Log("Boss activated: " + chosen.name);

                    yield return new WaitForSeconds(attackDuration);

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

                if (!bossHealth.isAlive)
                {
                    yield break;
                }

                if (teleportPoints.Length > 0)
                {
                    Transform chosenPoint = teleportPoints[Random.Range(0, teleportPoints.Length)];
                    transform.position = chosenPoint.position;
                    Debug.Log("Boss teleported to: " + chosenPoint.name);
                }
            }
        }
    }

    
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
        canAttack = false;
        canTeleport = false;

        anim.SetTrigger("Stagger");
        Debug.Log("Boss staggered!");

        yield return new WaitForSeconds(3f);

        canAttack = true;
        canTeleport = true;
    }

    private IEnumerator Death()
    {
        isAlive = false;

        // doorAnim.SetBool("isDead", true);
        anim.SetTrigger("Death");

        Debug.Log("Boss defeated!");

        yield return new WaitForSeconds(3f);

        if (bossHealth != null)
        {
            bossHealth.DestroySelf();
        }
    }
} 
