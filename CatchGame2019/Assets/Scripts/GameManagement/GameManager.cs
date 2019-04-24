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
    
    [SerializeField] private ObjectSpawner objectSpawner;

    GameLevelController gameLevelController;



    private void Start()
    {        
        DontDestroyOnLoad(gameObject);
        gameLevelController= new GameLevelController(levelConfig);
        gameLevelController.LevelCompleted += GameLevelController_OnLevelCompleted;
        gameLevelController.LevelFailed += GameLevelController_OnLevelFailed;

        canvasManager.StartNewGameClicked += CanvasManager_StartNewGameClicked;
        sceneController.NewGameStarted += SceneController_OnNewGameStarted;
        sceneController.BootSceneStarted += SceneController_OnBootSceneStarted;
    }

    public void CanvasManager_StartNewGameClicked()
    {        
        sceneController.StartNewGameScene();        
    }

    private void SceneController_OnNewGameStarted()
    {
        SetupNewGame();
    }

    private void SetupNewGame()
    {
        canvasManager.GameStartedHandler();

        gameLevelController.ParseLevelConfig();
        int[] spawnIntervals=GetSpawnIntervals();        
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
    }

    private void GameLevelController_OnLevelFailed()
    {
        Debug.Log("Game Over");
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

    //Change level function



}
