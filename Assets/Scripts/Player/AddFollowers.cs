using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFollowers : MonoBehaviour
{
    public List<GameObject> followers;

    // Start is called before the first frame update
    void Start()
    {
        followers = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    RaycastHit hit;
        //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        //    {
        //        if (hit.collider.CompareTag("Worker"))
        //        {
        //            Debug.Log("Worker found!");
        //        }
        //    }
        //}
    }
}
