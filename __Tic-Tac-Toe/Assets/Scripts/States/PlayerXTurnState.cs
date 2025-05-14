using Assets.Scripts.AI;
using Managers;
using Model;
using States.Abstraction;
using UnityEngine;

namespace States
{
    public class PlayerXTurnState : IGameState
    {
        private AIPlayer _aiPlayer;

        public void EnterState(GameManager gameManager)
        {
            
            gameManager.CurrentPlayer = PlayerMove.X;

            if (gameManager.playerXType == PlayerType.AI)
            {
                _aiPlayer = new AIPlayer(gameManager.difficulty);
                var chosenCell = _aiPlayer.MakeMove(gameManager);

                if (chosenCell != null)
                {
                    gameManager.MakeMoveWithDelay(chosenCell, .7f);
                }
            }
        }

        public void UpdateState(GameManager gameManager)
        {
            if (gameManager.CheckForWinner() || gameManager.moveCount >= 9)
                gameManager.SetState(new GameOverState());
            else
                gameManager.SetState(new PlayerOTurnState());
        }

        public void ExitState(GameManager gameManager)
        {
            // Clean up or reset any necessary variables or states here
        }
    }
}
