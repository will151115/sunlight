using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBossAttack : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public float animTime1;
    public float animTime2;
    private Vector2 currentPosition1;
    private Vector2 currentPosition2;
    public GameObject attack1collider;
    public GameObject attack2collider;
    public bool playerInRange;
    public Enemy bossHealth;
    private int chosen;
    private bool finished;

    IEnumerator Attack1() // smack down
    {
        if (bossHealth.isAlive)
        {
            if (playerInRange)
            {
                if (chosen == 1)
                {
                    Vector2 originalPos = transform.position;

                    
                    currentPosition1 = new Vector2(player.transform.position.x, player.transform.position.y + 4);
                    transform.position = currentPosition1;

                    anim.SetTrigger("smackDown");
                    attack1collider.SetActive(true);
                    yield return new WaitForSeconds(animTime1);
                    attack1collider.SetActive(false);

                    transform.position = originalPos; 
                    finished = true;
                }
            }
        }
    }


    IEnumerator Attack2() // dash
    {
        if (bossHealth.isAlive)
        {
            if (playerInRange)
            {
                if (chosen == 2)
                {
                    Vector2 originalPos = transform.position; // remember where the boss started

                    currentPosition2 = new Vector2(player.transform.position.x + 7, player.transform.position.y);
                    transform.position = currentPosition2;

                    anim.SetTrigger("dash");
                    attack2collider.SetActive(true);
                    yield return new WaitForSeconds(animTime2);
                    attack2collider.SetActive(false);

                    transform.position = originalPos; // return back exactly
                    finished = true;
                }
            }
        }
    }


    IEnumerator ChooseAttack()
    {
        while (bossHealth.isAlive)
        {
            if (playerInRange && finished)
            {
                chosen = Random.Range(1, 3);
                finished = false;
                if (chosen == 1)
                    yield return StartCoroutine(Attack1());

                else if (chosen == 2)
                    yield return StartCoroutine(Attack2());
            }
            yield return new WaitForSeconds(1f);
        }
        
        /* if(originalPos.x - player.transform.position.x >= 3)
            {
                StartCoroutine(Attack1);
            }
        */
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

    void Start()
    {
        finished = true;
        StartCoroutine(ChooseAttack());
    }
}
