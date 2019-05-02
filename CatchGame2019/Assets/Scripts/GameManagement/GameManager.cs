using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] TextAsset levelConfig;
    [SerializeField] private SceneController sceneController;    
    [SerializeField] private CanvasManager canvasManager;
    [SerializeField] private LevelTimer levelTimer;    
    [SerializeField] private ObjectSpawner objectSpawner;

    private GameLevelController gameLevelController;



    private void Start()
    {        
        DontDestroyOnLoad(gameObject);
        gameLevelController= new GameLevelController(levelConfig,levelTimer);
        sceneController.InitiateSceneController();
        gameLevelController.LevelCompleted += GameLevelController_OnLevelCompleted;
        gameLevelController.LevelFailed += GameLevelController_OnLevelFailed;
        gameLevelController.TimerOneSecLeft += GameLevelController_TimerOneSecLeft;

        canvasManager.StartNewGameClicked += CanvasManager_StartNewGameClicked;
        canvasManager.GoToNextLevelClicked += CanvasManager_GoToNextLevelClicked;
        canvasManager.GameOverClicked += CanvasManager_GameOverClicked;
        canvasManager.StartGameAgainClicked += CanvasManager_StartGameAgainClicked;

        sceneController.NewGameStarted += SceneController_OnNewGameStarted;
        sceneController.BootSceneStarted += SceneController_OnBootSceneStarted;

    }    

    public void CanvasManager_StartNewGameClicked()
    {        
        sceneController.StartNewGameScene();        
    }

    private void SceneController_OnNewGameStarted()
    {
        InitializeNewGame();
    }

    private void InitializeNewGame()
    {
        canvasManager.GameStartedHandler();
        gameLevelController.ParseLevelConfig();

        StartGame();
    }

    private void StartGame()
    {
        canvasManager.UpdateCurrentLevelText(gameLevelController.CurrentLevel);
        int[] spawnIntervals = GetSpawnIntervals();
        gameLevelController.StartNewTimer();
        objectSpawner.StartSpawning(spawnIntervals[0], spawnIntervals[1]);
    }

    private int[] GetSpawnIntervals()
    {
        return gameLevelController.GetCurrentSpawnIntervals();
    }

    private void SceneController_OnBootSceneStarted()
    {

    }

    private void GameLevelController_OnLevelCompleted()
    {
        Debug.Log("Level Completed");

        if (gameLevelController.CheckIfNewLevelsExist())
        {
            canvasManager.LevelCompletedHandler(gameLevelController.GetCurrentScore());
            canvasManager.UpdateCurrentLevelText(gameLevelController.CurrentLevel);

        }
        else
        {
            gameLevelController.ResetLevel();
            canvasManager.NoMoreLevelsHandler(gameLevelController.GetCurrentScore());
        }

             
    }

    private void GameLevelController_OnLevelFailed()
    {        
        Debug.Log("Game Over");
        canvasManager.LevelFailedHandler(gameLevelController.GetCurrentScore());
        gameLevelController.ResetLevel();
    }

    private void GameLevelController_TimerOneSecLeft()
    {
        objectSpawner.StopSpawning();
    }

    public void IncreaseScore(int amount)
    {
        gameLevelController.IncreaseScore(amount);
        canvasManager.UpdateScoreUI(gameLevelController.GetCurrentScore());
    }

    public void DecreaseScore(int amount)
    {
        gameLevelController.DecreaseScore(amount);
        canvasManager.UpdateScoreUI(gameLevelController.GetCurrentScore());
    }

    private void ResetScore()
    {
        canvasManager.UpdateScoreUI(0);
    }

    private void CanvasManager_GoToNextLevelClicked()
    {
        ChangeLevel();
    }

    private void ChangeLevel()
    {
        if (gameLevelController.CheckIfNewLevelsExist())
        {
            gameLevelController.ResetScore();
            ResetScore();
            StartGame();
        }
    }

    private void CanvasManager_GameOverClicked()
    {
        StartGame();
    }

    private void CanvasManager_StartGameAgainClicked()
    {
        StartGame();
    } 

}
