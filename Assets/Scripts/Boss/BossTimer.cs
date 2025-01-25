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
    [SerializeField] private AudioSource normalMusic;
    [SerializeField] private AudioSource bossMusic;
    [SerializeField] private AudioSource doorOpen;
    [SerializeField] private AudioSource doorSlam;
    [SerializeField] private AudioSource bossAngry;

    [SerializeField] private AudioClip[] bossAngryClips;

    private Detect detect;

    bool hasPlayedDoorOpen = false;
    bool hasPlayedBossAngry = false;

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
            if(!normalMusic.isPlaying) normalMusic.Stop();
            if(!bossMusic.isPlaying) bossMusic.Play();

            if (detectTime > 0f)
            {
                if (!bossAngry.isPlaying && !hasPlayedBossAngry)
                {
                    bossAngry.clip = bossAngryClips[Random.Range(0, bossAngryClips.Length)];
                    bossAngry.Play();
                    hasPlayedBossAngry = true;
                }

                if (!doorOpen.isPlaying && !hasPlayedDoorOpen)
                {
                    doorOpen.Play();
                    hasPlayedDoorOpen = true;
                }

                detectTime -= Time.deltaTime;
            }
            else
            {
                bossMusic.Stop();
                normalMusic.Play();
                door.SetActive(true);
                detect.enabled = false;
                doorSlam.Play();
                remainingTime = 10f;
                detectTime = 3f;
                hasPlayedDoorOpen = false;
                hasPlayedBossAngry = false;
            }
        }
    }
}
