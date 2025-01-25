using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Detect : MonoBehaviour
{
    public float detectionDistance = 8f; // Detection range
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(this.transform.position, player.position);

        if (distance < detectionDistance)
        {
            transform.LookAt(player);

            EndGame();
        }
    }

    
    private void EndGame()
    {
        Debug.Log("Player detected! Game Over.");

        // Uncomment the line below to load a game over scene
        // SceneManager.LoadScene("GameOverScene");

        // Or 
        // Application.Quit();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Player detected!");
        }
    }
}