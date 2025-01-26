using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossTimer : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private float remainingTime;
    [SerializeField] private float detectTime = 3f;
    [SerializeField] private AudioSource normalMusic;
    [SerializeField] private AudioSource bossMusic;
    [SerializeField] private AudioSource doorOpen;
    [SerializeField] private AudioSource doorSlam;
    [SerializeField] private AudioSource bossAngry;

    [SerializeField] private AudioClip[] bossAngryClips;

    [SerializeField] private Animator clockAnimator;

    private Detect detect;
    private Animator animator;

    bool hasPlayedDoorOpen = false;
    bool hasPlayedBossAngry = false;

    // Start is called before the first frame update
    void Start()
    {
        detect = GetComponent<Detect>();
        animator = GetComponent<Animator>();
        animator.Play("Leave", 0, 1f);
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
            clockAnimator.Play("Action");
        }
        else
        {
            timer.text = 0f.ToString();
            detect.enabled = true;
            if(!normalMusic.isPlaying) normalMusic.Stop();
            if(!bossMusic.isPlaying) bossMusic.Play();

            if (detectTime > 0f)
            {
                if (!bossAngry.isPlaying && !hasPlayedBossAngry)
                {
                    bossAngry.clip = bossAngryClips[Random.Range(0, bossAngryClips.Length)];
                    bossAngry.Play();
                    animator.Play("Peek");
                    doorAnimator.Play("SlamOpen");
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
                clockAnimator.Play("Idle");
                animator.Play("Leave");
                doorAnimator.Play("CloseSlam");
                bossMusic.Stop();
                normalMusic.Play();
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
