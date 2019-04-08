using UnityEngine.Events;
using UnityEngine;

public class Events
{
    [System.Serializable] public class EventGameState : UnityEvent<GameStateController.GameState, GameStateController.GameState> { }    
}
