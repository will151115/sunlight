using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator anim;

    public float fireDelay = 0.25f; // 👈 delay before bullet spawns

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ShootWithDelay());
        }
    }

    IEnumerator ShootWithDelay()
    {
        // play animation immediately
        anim.SetTrigger("shoot");

        // wait 0.25 seconds
        yield return new WaitForSeconds(fireDelay);

        // now spawn bullet
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
