using System;
using UnityEngine;
using UnityEngine.UI;

public class SimpleEventButton:MonoBehaviour
{
    public event Action Click;
    [SerializeField] UIEventButton simpleButton;

    private void Start()
    {
        simpleButton.Click += HandleClicked;
    }

    void HandleClicked()
    {
        if (Click != null)
        {
            Click();
        }     
    }

}