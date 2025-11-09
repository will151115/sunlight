using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    public Transform destination; // The portal this one connects to
    private bool canTeleport = true;
    private float cooldown = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canTeleport && other.CompareTag("Player"))
        {
            destination.GetComponent<Portal>().canTeleport = false;
            other.transform.position = destination.position;
            StartCoroutine(ResetCooldown());
        }
    }

    private IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canTeleport = true;
    }
}
