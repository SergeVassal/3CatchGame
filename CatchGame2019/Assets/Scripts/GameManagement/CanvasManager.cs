using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public event Action StartNewGameClicked;
    public event Action StartGameAgainClicked;
    public event Action GoToNextLevelClicked;
    public event Action GameOverClicked;

    [SerializeField] private SimpleEventButton startNewGameButton;
    [SerializeField] private Text scoreText;
    [SerializeField] private SimpleEventButton goToNextLevelButton;
    [SerializeField] private Text goToNextLevelText;
    [SerializeField] private SimpleEventButton gameOverButton;
    [SerializeField] private Text gameOverText;
    [SerializeField] private SimpleEventButton startGameAgainButton;
    [SerializeField] private Text noMoreLevelsText;
    [SerializeField] private Text currentLevelText;



    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        startNewGameButton.Click += StartNewGameClickedHandler;
        goToNextLevelButton.Click += GoToNextLevelClickedHandler;
        gameOverButton.Click += GameOverClickedHandler;
        startGameAgainButton.Click += StartGameAgainClickedHandler;

    }    

    private void StartNewGameClickedHandler()
    {
        if (StartNewGameClicked != null)
        {
            StartNewGameClicked();
        }
    }    

    public void GameStartedHandler()
    {
        startNewGameButton.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
    }

    public void LevelCompletedHandler(int score)
    {
        string levelComplete = string.Format("Level Completed!!! You scored: {0}  NEXT LEVEL", score);
        goToNextLevelText.text = levelComplete;
        goToNextLevelButton.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
    }

    private void GoToNextLevelClickedHandler()
    {
        goToNextLevelButton.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);

        if (GoToNextLevelClicked != null)
        {
            GoToNextLevelClicked();
        }
    }

    public void UpdateScoreUI(int newScore)
    {
        scoreText.text = newScore.ToString();
    }

    public void LevelFailedHandler(int score)
    {
        string gameOver = string.Format("GAME OVER!!! You scored: {0}   TRY AGAIN", score);
        gameOverText.text = gameOver;
        gameOverButton.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);

    }

    private void GameOverClickedHandler()
    {
        if (GameOverClicked != null)
        {
            gameOverButton.gameObject.SetActive(false);
            GameOverClicked();
        }
    }

    public void NoMoreLevelsHandler(int score)
    {
        noMoreLevelsText.gameObject.SetActive(true);
        string noMoreLevels = string.Format("You finished the game. You scored: {0}", score);
        noMoreLevelsText.text = noMoreLevels;
        startGameAgainButton.gameObject.SetActive(true);

        
    }

    private void StartGameAgainClickedHandler()
    {
        startGameAgainButton.gameObject.SetActive(false);
        noMoreLevelsText.gameObject.SetActive(false);

        if (StartGameAgainClicked != null)
        {
            StartGameAgainClicked();
        }
    }

    public void UpdateCurrentLevelText(int level)
    {
        currentLevelText.text = String.Format("Level: {0}",level.ToString());
    }


}
