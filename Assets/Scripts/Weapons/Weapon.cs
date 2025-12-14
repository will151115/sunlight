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

    public float doubleTapTime = 0.4f;

    public float meleeAnimTime = 0.5f;

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

        yield return new WaitForSeconds(0.2f);
        canShoot = true;
    }

    IEnumerator Melee()
    {
        if (!canMelee) yield break;
        canMelee = false;

        meleeTrigger.SetActive(true);
        anim.SetTrigger("melee");
        yield return new WaitForSeconds(meleeAnimTime);
        meleeTrigger.SetActive(false);

        yield return new WaitForSeconds(meleeCooldown);
        canMelee = true;
    }
}
