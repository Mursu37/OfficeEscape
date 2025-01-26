using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class SafetyCheck : MonoBehaviour
{
    [SerializeField] private GameObject safeImage;
    [SerializeField] private GameObject dangerImage;

    [SerializeField] private Transform boss;
    [SerializeField] private Transform player;

    [SerializeField] private LayerMask obstacleLayer;

    private float detectionDistance = 12;

    private void Update()
    {
        float distance = Vector3.Distance(boss.position, player.position);
        if (distance < detectionDistance)
        {
            Vector3 directionToPlayer = (player.position - boss.position).normalized;
            if (!Physics.Raycast(boss.transform.position, directionToPlayer, detectionDistance, obstacleLayer))
            {
                Danger();
                return;
            }
        }
        Safe();
    }
        private void Safe()
        {
            safeImage.SetActive(true);
            dangerImage.SetActive(false);
            
        }

        private void Danger()
        {
            safeImage.SetActive(false);
            dangerImage.SetActive(true);
        }
}
