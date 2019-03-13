using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SceneController))]
[RequireComponent(typeof(GameStateController))]
[RequireComponent(typeof(CanvasManager))]
public class GameManager : Singleton<GameManager>
{
    private SceneController sceneController;
    private GameStateController gameStateController;
    private CanvasManager canvasManager;


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
    }

}
