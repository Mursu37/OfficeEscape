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
    private Quaternion defaultRotation;

    [SerializeField] private Animator animator;

    private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;

        animator.Play("Typing");
        animator.SetBool("IsSitting", true);
        animator.SetBool("IsTyping", true);
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

            StartCoroutine(StandUp());

            if (canMove)
            {
                agent.enabled = true;
                if (currentIndex > 0)
                {
                    GameObject previousFollower = addFollowers.followers[currentIndex - 1];
                    agent.SetDestination(previousFollower.transform.position);
                }
                else
                {
                    agent.SetDestination(player.position);
                }

                if (agent.velocity.magnitude > 0f)
                {
                    animator.SetBool("IsMoving", true);
                }
                else
                {
                    animator.SetBool("IsMoving", false);
                }
            }
        }
    }

    private IEnumerator StandUp()
    {
        animator.SetBool("IsSitting", false);
        animator.SetBool("IsTyping", false);
        yield return new WaitForSeconds(2f);
        canMove = true;
    }

    private IEnumerator SitDown()
    {
        yield return new WaitForSeconds(3f);
        agent.enabled = false;
        transform.position = defaultPosition;
        transform.rotation = defaultRotation;
        animator.SetBool("IsSitting", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("IsTyping", true);
    }


    public void FollowerDetected()
    {
        canMove = false;
        addFollowers.RemoveFollower(gameObject);
        agent.SetDestination(defaultPosition);
        StartCoroutine(SitDown());
    }
}
