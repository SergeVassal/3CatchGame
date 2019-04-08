using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{    
    public enum GameState
    {
        PREGAME,
        LOADING,
        RUNNING,
        PAUSED,
        GAMEOVER
    }
    public GameState CurrentGameState
    {
        get { return currentGameState; }
        private set { currentGameState = value; }
    }

    private GameState currentGameState = GameState.PREGAME;
    

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeGameStateToLoading()
    {
        UpdateState(GameState.LOADING);
    }

    public void ChangeGameStateToRunning()
    {
        UpdateState(GameState.RUNNING);
    }

    public void ChangeGameStateToPregame()
    {
        UpdateState(GameState.PREGAME);
    }

    private void UpdateState(GameState state)
    {
        GameState previousGameState = currentGameState;
        currentGameState = state;

        GameManager.Instance.CallOnGameStateChangedEvent(currentGameState, previousGameState);
        
    }
}
