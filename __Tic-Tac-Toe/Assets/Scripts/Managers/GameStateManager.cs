using States;
using States.Abstraction;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class GameStateManager
    {
        public GameState CurrentGameState;

        public GameState XTurnState { get; private set; }
        public GameState OTurnState { get; private set; }
        public GameState GameOverState { get; private set; }

        [Inject]
        public GameStateManager(
            [Inject(Id = "PlayerX")] GameState xTurnState,
            [Inject(Id = "PlayerO")] GameState oTurnState,
            [Inject(Id = "GameOver")] GameState gameOverState)
        {
            XTurnState = xTurnState;
            OTurnState = oTurnState;
            GameOverState = gameOverState;
        }

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