using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    
    
        
    
    
  

   

    private void Update()
    {
        if (currentGameState==GameState.PREGAME)
        {
            return;
        }

        if (currentGameState == GameState.LOADING)
        {
            UIManager.Instance.loadingScreen.UpdateLoadingSlider(ao.progress);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

            

    

    

    

    
    

   

    public void StateToPausedInventory()
    {
        
        UpdateState(GameState.PAUSEDINVENTORY);
    }

    public void StateToRunning()
    {        
        UpdateState(GameState.RUNNING);
        
    }

    public void TogglePause()
    {        
        UpdateState(currentGameState==GameState.RUNNING?GameState.PAUSED:GameState.RUNNING);
    }

    public void RestartGame()
    {
        UpdateState(GameState.PREGAME);
    }

    public void StateToGameOver()
    {
        UnloadLevel("01Level1");              
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
