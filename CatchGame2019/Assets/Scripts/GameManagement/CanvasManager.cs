using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public event Action StartNewGameClicked;

    [SerializeField] private SimpleEventButton startNewGameButton;
    [SerializeField] private Text scoreText;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        startNewGameButton.Click += StartNewGameClickedHandler;        
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
    }

    public void UpdateScoreUI(int newScore)
    {
        scoreText.text = newScore.ToString();
    }
}
