using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddFollowers : MonoBehaviour
{
    public List<GameObject> followers;
    [SerializeField] private GameObject hold;
    [SerializeField] private Image holdFill;
    [SerializeField] private float holdTime;

    [SerializeField] private AudioSource popping;

    private Animator animator;

    private bool hasPlayedPopping = false;

    // Start is called before the first frame update
    void Start()
    {
        followers = new List<GameObject>();
        holdTime = 0f;
        animator = GetComponent<Animator>();
    }

    public void HandleFollower(GameObject follower)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, follower.transform.position);
        var outline = follower.GetComponent<Outline>();
        if (distanceToPlayer < 2f)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, 
                    LayerMask.GetMask("Worker", "Outlined")))
            {
                outline.AddOutline();
                hold.gameObject.SetActive(true);
                if (Input.GetButton("Fire1"))
                {
                    if (popping != null && !popping.isPlaying && !hasPlayedPopping)
                    {
                        popping.Play();
                        hasPlayedPopping = true;
                    }
                    holdTime += Time.deltaTime;
                    holdFill.fillAmount = holdTime;
                    animator.SetBool("IsTalking", true);
                    if (holdTime >= 1f)
                    {
                        animator.SetBool("IsTalking", false);
                        followers.Add(follower);
                        holdTime = 0f;
                        holdFill.fillAmount = 0f;
                        hold.gameObject.SetActive(false);
                        outline.RemoveOutline();
                    }
                }
                else
                {
                    holdTime = 0f;
                    //outline.RemoveOutline();
                    popping.Stop();
                    hasPlayedPopping = false;
                }
            }
            else
            {
                hold.gameObject.SetActive(false);
                outline.RemoveOutline();
                hasPlayedPopping = false;
                //if(popping.isPlaying) popping.Stop();
            }
        }
        else
        {
            //outline.RemoveOutline();
            //if(popping.isPlaying) popping.Stop();
        }
    }

    public void RemoveFollower(GameObject follower)
    {
        followers.Remove(follower);
    }
}
