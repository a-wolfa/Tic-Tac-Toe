using States.Abstraction;

namespace Managers
{
    public class GameStateManager
    {
        public IGameState CurrentGameState { get; private set; }

        public void SetState(IGameState newState, GameManager gameManager)
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