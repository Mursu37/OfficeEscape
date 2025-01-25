using System;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire2"))
            {
                HandleMovement();
            }
        }

        private void HandleMovement()
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                _agent.SetDestination(hit.point);
            }
        }
    }
}
