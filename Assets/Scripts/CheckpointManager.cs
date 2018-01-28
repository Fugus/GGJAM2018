using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckpointManager : MonoBehaviour
{
    public UnityEngine.UI.Text[] timerTexts;

    public float remainingTime = 70f;

    public UnityEngine.UI.Text[] PlayDurations;

    public UnityEvent Lost;

    public float playTime = 0f;

    // Update is called once per frame
    void Update()
    {
        playTime += Time.deltaTime;

        remainingTime -= Time.deltaTime;
        if (remainingTime < 0f)
            remainingTime = 0f;
        foreach (UnityEngine.UI.Text timerText in timerTexts)
        {
            timerText.text = String.Format("{0:D2}:{1:D2}", Mathf.FloorToInt(remainingTime / 60), Mathf.FloorToInt(remainingTime % 60));
        }

        if (remainingTime <= 0f)
        {
            foreach (UnityEngine.UI.Text PlayDuration in PlayDurations)
            {
                PlayDuration.text = String.Format("Total time: {0:d2}:{1:d2}", Mathf.FloorToInt(playTime / 60), Mathf.FloorToInt(playTime % 60));
            }
            Lost.Invoke();
            enabled = false;
        }
    }
}
