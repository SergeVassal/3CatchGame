using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject startNewGameButton;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        AddGameStateListener();
    }

    private void AddGameStateListener()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameStateController.GameState currentState, GameStateController.GameState previousState)
    {
        startNewGameButton.SetActive(currentState == GameStateController.GameState.PREGAME);        
    }
}
