using System.Collections.Generic;
using Player;
using UnityEngine;

public class Detect : MonoBehaviour
{
    public float detectionDistance = 8f;
    public Transform player;
    public LayerMask obstacleLayer;
    [SerializeField] private AddFollowers addFollowers;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Movement movement;

    private bool playerDetected = false;
    private List<GameObject> detectedFollowers = new List<GameObject>();

    [SerializeField] private float timeSinceLastRaycast = 0f;

    private void Start()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        timeSinceLastRaycast = 0f;
    }

    void Update()
    {
        timeSinceLastRaycast += Time.deltaTime;

        if (timeSinceLastRaycast >= 1)
        {
            timeSinceLastRaycast = 0f;

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
                            detectedFollowers.Add(follower);
                        }
                    }
                }

                HandleDetectedFollowers();
            }
        }
    }

    private void HandleDetectedFollowers()
    {
        foreach (var follower in detectedFollowers)
        {
            follower.GetComponent<WorkerFollow>().FollowerDetected();
        }
        detectedFollowers.Clear();
    }

    private void EndGame()
    {
        Debug.Log("Player detected! Game Over.");
        gameOver.SetActive(true);
        movement.enabled = false;
        Time.timeScale = 0f;
    }
}
