using States.Abstraction;

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
    }
}