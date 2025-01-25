using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Detect : MonoBehaviour
{
    public float detectionDistance = 8f; 
    public Transform player;
    public LayerMask obstacleLayer; 
    private bool playerDetected = false;

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(this.transform.position, player.position);

        if (distance < detectionDistance)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            if (!Physics.Raycast(transform.position, directionToPlayer, detectionDistance, obstacleLayer))
            {
                
                transform.LookAt(player);

                if (!playerDetected)
                {
                    EndGame();
                    playerDetected = true; 
                }
            }
        }
        else
        {
            playerDetected = false; 
        }
    }

    private void EndGame()
    {
        Debug.Log("Player detected! Game Over.");
        // Uncomment the line below to load a game over scene
        // SceneManager.LoadScene("GameOverScene");

        // Or quit the application
        // Application.Quit();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            Vector3 distVector = other.transform.position - transform.position;
            distVector.y = 0; // Keep the y component zero for horizontal rotation
            Quaternion angle = Quaternion.LookRotation(distVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, angle, Time.deltaTime * 5f); // Smooth rotation
        }
    }
}