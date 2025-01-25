using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{
    public Transform player; 

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 8f) // Detection range
        {
            // Rotate to look at the player
            transform.LookAt(player);
        }
    }

    // Optional: Debugging using OnTriggerStay
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Player detected!");
        }
    }
}