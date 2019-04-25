using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : Singleton<LevelTimer>
{
    private float timeStep = 1f;
    private float elapsed;
    private bool timerEnabled;

    public event Action TimerTicked;


    public void StartTimer()
    {
        elapsed = 0;
        timerEnabled = true;
    }

    public void StopTimer()
    {
        elapsed = 0;
        timerEnabled = false;
    }



    private void Update()
    {
        if (timerEnabled)
        {
            elapsed += Time.deltaTime;

            if (elapsed >= timeStep)
            {
                elapsed = 0;
                if (TimerTicked != null)
                {
                    TimerTicked();
                }
            }
        }
        
    }

}
