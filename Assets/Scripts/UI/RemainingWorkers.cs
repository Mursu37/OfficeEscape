using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RemainingWorkers : MonoBehaviour
{
    public int maxWorkers;
    [SerializeField] private AddFollowers addFollowers;
    [SerializeField] private Animator elevatorDoor;
    public bool canUseElevator = false;
    private TextMeshProUGUI remaining;

    [SerializeField] private AudioSource elevatorBell;

    // Start is called before the first frame update
    void Start()
    {
        remaining = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        remaining.text = addFollowers.followers.Count.ToString() + "/" + maxWorkers;

        if (addFollowers.followers.Count >= maxWorkers)
        {
            if (!canUseElevator && elevatorBell != null) elevatorBell.Play();
            canUseElevator = true;
        }
        else
        {
            canUseElevator = false;
        }

        elevatorDoor.SetBool("isOpen", canUseElevator);
    }
}
