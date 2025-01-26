using System;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Animator animator;

        [SerializeField] AudioSource walkSoundSource;
        [SerializeField] AudioClip[] walkSounds;

        private float walkSoundCooldown;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_agent.velocity.magnitude > 0f)
            {
                if (!walkSoundSource.isPlaying && walkSoundCooldown <= 0f)
                {
                    walkSoundSource.clip = walkSounds[UnityEngine.Random.Range(0, walkSounds.Length)];
                    walkSoundSource.Play();
                    walkSoundCooldown = 0.3f;
                }

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

            if (walkSoundCooldown > 0f)
            {
                walkSoundCooldown -= Time.deltaTime;
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
