using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SceneController))]
[RequireComponent(typeof(GameStateController))]
[RequireComponent(typeof(CanvasManager))]
[RequireComponent(typeof(GameLevelController))]
public class GameManager : Singleton<GameManager>
{
    public Events.EventGameState OnGameStateChanged;

    private SceneController sceneController;
    private GameStateController gameStateController;
    private CanvasManager canvasManager;
    private GameLevelController gameLevelController;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        InitiateGameSystemComponents();
    }

    private void InitiateGameSystemComponents()
    {
        sceneController = GetComponent<SceneController>();
        gameStateController = GetComponent<GameStateController>();
        canvasManager = GetComponent<CanvasManager>();
        gameLevelController = GetComponent<GameLevelController>();
    }

    public void StartNewGame()
    {
        gameStateController.ChangeGameStateToLoading();
        sceneController.StartNewGameScene();        
    }

    public void SetupNewGame()
    {

        gameStateController.ChangeGameStateToRunning();
    }

    public void StartBootScene()
    {
        gameStateController.ChangeGameStateToPregame();
    }

    public void CallOnGameStateChangedEvent(GameStateController.GameState currentState, GameStateController.GameState previousState)
    {
        OnGameStateChanged.Invoke(currentState, previousState);
    }

}
