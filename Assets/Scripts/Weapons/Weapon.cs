using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabWeapon : MonoBehaviour
{
    [Header("References")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator anim;
    public GameObject meleeTrigger;

    [Header("Settings")]
    public float fireDelay = 0.25f;
    public float meleeCooldown = 1f;

    private bool canShoot = true;
    private bool canMelee = true;

    private int timesClicked;
    private float lastTapTime = 0f;
    public float doubleTapTime = 0.4f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Shoot());
        }

        if (Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(Melee());
        }
    }

    IEnumerator Shoot()
    {
        if (!canShoot) yield break;
        canShoot = false;

        anim.SetTrigger("shoot");
        yield return new WaitForSeconds(fireDelay);

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Small optional delay between shots
        yield return new WaitForSeconds(0.2f);
        canShoot = true;
    }

    IEnumerator Melee()
    {
        if (!canMelee) yield break;
        canMelee = false;

        meleeTrigger.SetActive(true);
        

        meleeTrigger.SetActive(false);

        yield return new WaitForSeconds(meleeCooldown);
        canMelee = true;
    }
}
