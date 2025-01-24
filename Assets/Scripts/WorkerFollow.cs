using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
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
        agent.SetDestination(target.position);
    }
}
