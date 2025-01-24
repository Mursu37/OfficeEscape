using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect :
public transform Player;
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerStay(Collider collider)
    {
        // Debug.Log("Find");
        if (col.gameObject.name == "player")
        {
            transform.LookAt(player);
            transform.Translate(0, 0, 0.3f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
