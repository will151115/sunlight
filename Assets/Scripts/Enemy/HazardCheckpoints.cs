using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardCheckpoints : MonoBehaviour
{
    public GameObject player;
    public Transform currentCheckpoint;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            currentCheckpoint.transform.position = gameObject.transform.position;
        }
    }
}
