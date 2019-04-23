using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public event Action StartNewGameClicked;

    [SerializeField] private SimpleEventButton startNewGameButton;

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
}
