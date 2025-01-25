using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorTrigger : MonoBehaviour
{
    [SerializeField] private RemainingWorkers remainingWorkers;
    [SerializeField] private GameObject victory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (remainingWorkers.canUseElevator)
            {
                victory.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}
