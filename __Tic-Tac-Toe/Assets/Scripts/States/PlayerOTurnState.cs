using Managers;
using Model;
using States.Abstraction;
using UnityEngine;

namespace States
{
    public class PlayerOTurnState : IGameState
    {
        public void EnterState(GameManager gameManager)
        {
            Debug.Log("Player O's Turn");
            gameManager.CurrentPlayer = PlayerMove.O;

            if (gameManager.PlayerOType == PlayerType.AI)
            {
                var aiPlayer = new AIPlayer(gameManager.difficulty);
                var chosenCell = aiPlayer.MakeMove(gameManager);
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
            {
                gameManager.SetState(new PlayerXTurnState());
            }
        }

        public void ExitState(GameManager gameManager)
        {
            // Clean up or Reset
        }
    }
}
