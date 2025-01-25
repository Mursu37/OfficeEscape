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

    // Start is called before the first frame update
    void Start()
    {
        followers = new List<GameObject>();
        holdTime = 0f;
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
                hold.gameObject.SetActive(true);
                if (Input.GetButton("Fire1"))
                {
                    //if (popping != null && !popping.isPlaying)
                    //{
                    //    popping.Play();
                    //}
                    outline.AddOutline();
                    holdTime += Time.deltaTime;
                    holdFill.fillAmount = holdTime;
                    if (holdTime >= 1f)
                    {
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
                    outline.RemoveOutline();
                    //popping.Stop();
                }
            }
            else
            {
                hold.gameObject.SetActive(false);
                outline.RemoveOutline();
                //if(popping.isPlaying) popping.Stop();
            }
        }
        else
        {
            outline.RemoveOutline();
            //if(popping.isPlaying) popping.Stop();
        }
    }
}
