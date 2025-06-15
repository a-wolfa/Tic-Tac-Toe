using Managers;
using Model;
using Zenject;

namespace States.Abstraction
{
    public abstract class GameState
    {
        public abstract void EnterState(GameManager gameManager);

        public virtual void UpdateState(GameManager gameManager)
        {
            if (gameManager.board.GetModel().CheckWin() != PMove.None)
            {
                gameManager.GameStateManager.SetState(gameManager.GameStateManager.GameOverState, gameManager);
            }
        }
        public abstract void ExitState(GameManager gameManager);
    }
}
