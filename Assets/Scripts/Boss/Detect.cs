using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{
    public float detectionDistance = 8f;
    public Transform player;
    public LayerMask obstacleLayer;
    [SerializeField] private AddFollowers addFollowers;

    private bool playerDetected = false;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < detectionDistance)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            if (!Physics.Raycast(transform.position, directionToPlayer, detectionDistance, obstacleLayer))
            {
                Debug.Log("Player detected");
                SmoothLookAt(player.position);

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
                            Debug.Log("Follower detected");
                            SmoothLookAt(follower.transform.position);

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