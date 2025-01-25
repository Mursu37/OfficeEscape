using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private FollowerList followerList;
    private NavMeshAgent agent;
    private LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        layerMask = LayerMask.GetMask("Worker");
    }

    // Update is called once per frame
    void Update()
    {
        if (!followerList.followers.Contains(gameObject))
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer < 1f)
            {
                followerList.followers.Add(gameObject);
            }
        }
        else
        {
            int currentIndex = followerList.followers.IndexOf(gameObject);

            if (currentIndex > 0)
            {
                GameObject previousFollower = followerList.followers[currentIndex - 1];
                agent.SetDestination(previousFollower.transform.position);
            }
            else
            {
                agent.SetDestination(player.position);
            }
        }
        
    }
}
