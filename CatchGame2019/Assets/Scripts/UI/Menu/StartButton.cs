using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private Button StartNewGameButton;
   

    private void Start()
    {
        StartNewGameButton.onClick.AddListener(HandleStartNewGameClicked);        
    }

    void HandleStartNewGameClicked()
    {
        GameManager.Instance.StartNewGame();        
    }

}