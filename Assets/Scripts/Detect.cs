using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{
    public float detectionDistance = 8f;
    public Transform player;
    public LayerMask obstacleLayer;

    private bool playerDetected = false;
    private Vector3 lastPlayerPosition;
    private float movementThreshold = 0.1f;

    void Start()
    {
        lastPlayerPosition = player.position;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        bool isPlayerMoving = Vector3.Distance(player.position, lastPlayerPosition) > movementThreshold;

        if (distance < detectionDistance && isPlayerMoving)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            if (!Physics.Raycast(transform.position, directionToPlayer, detectionDistance, obstacleLayer))
            {
                SmoothLookAt(player.position);

                if (!playerDetected)
                {
                    EndGame();
                    playerDetected = true;
                }
            }
        }
        else if (distance >= detectionDistance || Physics.Raycast(transform.position, (player.position - transform.position).normalized, detectionDistance, obstacleLayer))
        {
            playerDetected = false;
        }

        lastPlayerPosition = player.position;
    }

    private void SmoothLookAt(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    private void EndGame()
    {
        Debug.Log("Player detected! Game Over.");
    }
}