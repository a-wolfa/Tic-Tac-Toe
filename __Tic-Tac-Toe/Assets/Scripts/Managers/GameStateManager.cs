using States;
using States.Abstraction;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class GameStateManager
    {
        public GameState CurrentGameState;

        public void SetState(GameState newState, GameManager gameManager)
        {
            CurrentGameState?.ExitState(gameManager);
            CurrentGameState = newState;
            CurrentGameState.EnterState(gameManager);
        }

        public void UpdateState(GameManager gameManager)
        {
            CurrentGameState?.UpdateState(gameManager);
        }
    }
}