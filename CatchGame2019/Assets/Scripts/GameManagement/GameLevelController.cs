using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System;

public class GameLevelController 
{    
    public event Action LevelCompleted;
    public event Action LevelFailed;

    private LevelConfigParser levelConfigParser;
    
    private TextAsset levelConfig;
    private int currentLevel=0;
    private int spawnIntervalMin;
    private int spawnIntervalMax;
    private int minLevelPassScore;
    private int currentTimer;
    private int currentScore;
    private Timer levelTimer;
    

    
    public GameLevelController(TextAsset levelConfigFile)
    {
        levelConfig = levelConfigFile;
        levelTimer = new Timer(1000);
    }


    public void ParseLevelConfig()
    {
        levelConfigParser = new LevelConfigParser(levelConfig);

        levelConfigParser.ParseLevelConfigJSON();
    }



    private void SetLevelVariables(int currentLevel)
    {
        spawnIntervalMin = levelConfigParser.GetSpawnIntervalMin(currentLevel);
        spawnIntervalMax = levelConfigParser.GetSpawnIntervalMax(currentLevel);
        minLevelPassScore = levelConfigParser.GetMinLevelPassScore(currentLevel);
        currentTimer = levelConfigParser.GetCurrentTimer(currentLevel);        
    }


    public int[] GetCurrentSpawnIntervals()
    {
        SetLevelVariables(currentLevel);
        return new int[2] { spawnIntervalMin, spawnIntervalMax };
    }

    public void StartNewTimer()
    {

        levelTimer.Elapsed += new ElapsedEventHandler(OnLevelTimerElapsedEvent);
        levelTimer.Enabled = true;       
        
    }

    private void OnLevelTimerElapsedEvent(object sender, ElapsedEventArgs e)
    {
        currentTimer -= 1;
        if (currentTimer <= 0)
        {
            levelTimer.Stop();
            CheckIfLevelCompleted();
        }
        Debug.Log("currentTimer "+ currentTimer);       


    }

    public void IncreaseScore(int amount)
    {
        currentScore += amount;
    }

    public void DecreaseScore(int amount)
    {
        currentScore -= amount;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }



    private void CheckIfLevelCompleted()
    {
        if (currentScore >= minLevelPassScore)
        {
            if (LevelCompleted != null)
            {
                LevelCompleted();
                currentLevel += 1;
            }
        }
        else
        {
            if (LevelFailed != null)
            {                
                LevelFailed();
                currentLevel = 0;
            }
        }
    }


}
