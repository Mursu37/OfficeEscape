using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Rendering;

public class Detect : MonoBehaviour
{
    public float detectionDistance = 8f;
    public Transform player;
    public LayerMask obstacleLayer;
    [SerializeField] private AddFollowers addFollowers;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Movement movement;

    private bool playerDetected = false;

    private void Start()
    {
        enabled = false;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < detectionDistance)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            if (!Physics.Raycast(transform.position, directionToPlayer, detectionDistance, obstacleLayer))
            {

                if (!playerDetected)
                {
                    EndGame();
                    playerDetected = true;
                }
            }
            else
            {
                foreach (GameObject follower in addFollowers.followers)
                {
                    if (follower != null)
                    {
                        Vector3 directionToFollower = (follower.transform.position - transform.position).normalized;
                        if (!Physics.Raycast(transform.position, directionToFollower, detectionDistance, obstacleLayer))
                        {

                            if (!playerDetected)
                            {
                                EndGame();
                                playerDetected = true;
                            }
                        }
                    }
                }
            }
        }
    }

    private void EndGame()
    {
        Debug.Log("Player detected! Game Over.");
        gameOver.SetActive(true);
        movement.enabled = false;
        Time.timeScale = 0f;
    }
}