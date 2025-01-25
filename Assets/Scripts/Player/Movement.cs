using System;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Animator animator;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_agent.velocity.magnitude > 0f)
            {
                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }

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
