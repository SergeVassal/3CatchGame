using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public sealed class UIEventButton:MonoBehaviour
{
    public event Action Click;
    [SerializeField] private Button button;



    private void Start()
    {
        button.onClick.AddListener(OnClickHandler);
    }

    private void OnClickHandler()
    {
        if (Click != null)
        {
            Click();
        }
    }
}
