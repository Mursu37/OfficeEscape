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

    // Start is called before the first frame update
    void Start()
    {
        followers = new List<GameObject>();
        holdTime = 0f;
    }

    public void HandleFollower(GameObject follower)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, follower.transform.position);
        if (distanceToPlayer < 2f)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("Worker")))
            {
                hold.gameObject.SetActive(true);
                if (Input.GetButton("Fire1"))
                {
                    holdTime += Time.deltaTime;
                    holdFill.fillAmount = holdTime;
                    if (holdTime >= 1f)
                    {
                        followers.Add(follower);
                        holdTime = 0f;
                        holdFill.fillAmount = 0f;
                        hold.gameObject.SetActive(false);
                    }
                }
                else
                {
                    holdTime = 0f;
                }
            }
            else
            {
                hold.gameObject.SetActive(false);
            }
        }
    }
}
