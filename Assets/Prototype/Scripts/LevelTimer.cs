using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    private bool timerActive = false;
    [HideInInspector]public float totalTime;
    [HideInInspector]public float timer;

    public event Action TimerFinishedEvent;

    private void Start()
    {
        StartTimer(5f);
    }

    private void Update()
    {
        if (timerActive)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                TimerFinishedEvent?.Invoke();
                timerActive = false;
            }
        }
    }

    public void StartTimer(float seconds)
    {
        totalTime = seconds;
        timer = totalTime;
        timerActive = true;
    }
}