using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private SceneController sceneController;    
    [SerializeField] private CanvasManager canvasManager;
    [SerializeField] private GameLevelController gameLevelController;
    [SerializeField] private ObjectSpawner objectSpawner;
     

    
    private void Start()
    {        
        DontDestroyOnLoad(gameObject);
        
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
        float[] spawnIntervals=GetSpawnIntervals();
        objectSpawner.StartSpawning(spawnIntervals[0], spawnIntervals[1]);
    }

    private float[] GetSpawnIntervals()
    {
        return gameLevelController.GetCurrentSpawnIntervals();
    }

    private void SceneController_OnBootSceneStarted()
    {

    }

    

}
