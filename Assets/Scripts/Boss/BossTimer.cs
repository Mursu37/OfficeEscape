using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private float remainingTime;
    [SerializeField] private GameObject door;
    [SerializeField] private float detectTime = 3f;

    private Detect detect;

    // Start is called before the first frame update
    void Start()
    {
        detect = GetComponent<Detect>();
        detectTime = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;
            int timeAsInt = Mathf.FloorToInt(remainingTime);
            timer.text = timeAsInt.ToString();
        }
        else
        {
            timer.text = 0f.ToString();

            door.SetActive(false);
            detect.enabled = true;

            if (detectTime > 0f)
            {
                detectTime -= Time.deltaTime;
            }
            else
            {
                door.SetActive(true);
                detect.enabled = false;
                remainingTime = 10f;
                detectTime = 3f;
            }
        }
    }
}
