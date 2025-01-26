using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class WorkerFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private AddFollowers addFollowers;
    private NavMeshAgent agent;
    private Vector3 defaultPosition;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        defaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!addFollowers.followers.Contains(gameObject))
        {
            addFollowers.HandleFollower(gameObject);
        }
        else
        {
            int currentIndex = addFollowers.followers.IndexOf(gameObject);

            if (currentIndex > 0)
            {
                GameObject previousFollower = addFollowers.followers[currentIndex - 1];
                agent.SetDestination(previousFollower.transform.position);
            }
            else
            {
                agent.SetDestination(player.position);
            }
        }
    }
    
    public void FollowerDetected()
    {
        addFollowers.RemoveFollower(gameObject);
        agent.SetDestination(defaultPosition);
    }
}
