using Assets.Scripts.AI;
using Managers;
using Model;
using States.Abstraction;

namespace States
{
    public class PlayerOTurnState : IGameState
    {
        private readonly float _delay = 0.7f;

        public void EnterState(GameManager gameManager)
        {
            gameManager.CurrentPlayer = PlayerMove.O;

            if (gameManager.playerOType == PlayerType.AI)
            {
                var aiPlayer = new AIPlayer(gameManager.difficulty);
                var chosenCell = aiPlayer.MakeMove(gameManager);
                if (chosenCell != null)
                {
                    gameManager.MakeMoveWithDelay(chosenCell, _delay);
                }
            }
        }

        public void UpdateState(GameManager gameManager)
        {
            if (gameManager.CheckForWinner() || gameManager.moveCount >= 9)
                gameManager.SetState(new GameOverState());
            else
            {
                gameManager.SetState(new PlayerXTurnState());
            }
        }

        public void ExitState(GameManager gameManager) { }
    }
}
