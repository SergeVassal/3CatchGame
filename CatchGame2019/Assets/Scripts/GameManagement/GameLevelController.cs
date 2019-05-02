using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System;

public class GameLevelController 
{
    public event Action TimerOneSecLeft;
    public event Action LevelCompleted;
    public event Action LevelFailed;

    public int CurrentLevel { get; private set; }

    private LevelConfigParser levelConfigParser;    
    private TextAsset levelConfig;
    private int spawnIntervalMin;
    private int spawnIntervalMax;
    private int minLevelPassScore;
    private int currentTimer;
    private int currentScore;   
    private int levelCount;
    private LevelTimer timer;
    

    
    public GameLevelController(TextAsset levelConfigFile,LevelTimer levelTimer)
    {
        levelConfig = levelConfigFile;
        timer = levelTimer;
        timer.TimerTicked += OnLevelTimerTicked;        
    }

    public void ParseLevelConfig()
    {
        levelConfigParser = new LevelConfigParser(levelConfig);

        levelConfigParser.ParseLevelConfigJSON();
    }

    private void SetLevelVariables(int currentLevel)
    {
        CurrentLevelInfoDTO currentLevelInfo = levelConfigParser.GetCurrentLevelInfoDTO(currentLevel);
        spawnIntervalMin = currentLevelInfo.spawnIntervalMin;
        spawnIntervalMax = currentLevelInfo.spawnIntervalMax;
        minLevelPassScore = currentLevelInfo.minLevelPassScore;
        currentTimer = currentLevelInfo.timer;
        levelCount = currentLevelInfo.levelCount;        
    }

    public int[] GetCurrentSpawnIntervals()
    {
        SetLevelVariables(CurrentLevel);
        return new int[2] { spawnIntervalMin, spawnIntervalMax };
    }

    public void StartNewTimer()
    {
        timer.StartTimer();  
    }

    private void OnLevelTimerTicked()
    {
        currentTimer -= 1;
        if (currentTimer <= 1)
        {
            if (TimerOneSecLeft != null)
            {
                TimerOneSecLeft();
            }
        }
        if (currentTimer <= 0)
        {            
            timer.StopTimer();
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
        if (currentScore <= 0)
        {
            currentScore = 0;
        }
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void ResetLevel()
    {
        CurrentLevel = 0;
        currentScore = 0;
    }

    private void CheckIfLevelCompleted()
    {
        if (currentScore >= minLevelPassScore)
        {
            if (LevelCompleted != null)
            {
                CurrentLevel += 1;
                LevelCompleted();                
            }
        }
        else
        {
            if (LevelFailed != null)
            {                
                LevelFailed();                
            }
        }
    }    

    public bool CheckIfNewLevelsExist()
    {
        if (CurrentLevel >= levelCount)
        {
            Debug.Log("No more levels!");
            return false;
        }
        else
        {
            Debug.Log("Success, next level is  " + CurrentLevel);
            return true;
        }
    }

    public void ResetScore()
    {       
        currentScore = 0;        
    }
}
