using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private FollowerList followerList;
    [SerializeField] private float holdTime;
    private NavMeshAgent agent;
    private LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        layerMask = LayerMask.GetMask("Worker");
        holdTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!followerList.followers.Contains(gameObject))
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer < 2f)
            {
                if (Input.GetButton("Fire1"))
                {
                    RaycastHit hit;
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                    {
                        if (hit.collider.CompareTag("Worker"))
                        {
                            holdTime += Time.deltaTime;
                            if (holdTime >= 1f)
                            {
                                followerList.followers.Add(gameObject);
                            }
                        }
                    }
                }
                else
                {
                    holdTime = 0f;
                }
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
