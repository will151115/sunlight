using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool activated;
    public Sprite activatedSprite;
    public SpriteRenderer sr;
    public Animator anim;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Activate();
        }

        if (other.CompareTag("MeleeTrigger"))
        {
            Activate();
        }
    }

    public void Activate()
    {
        activated = true;
        anim.SetBool("activated", true);
        sr.sprite = activatedSprite;
    }
}
