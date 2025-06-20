using Assets.Scripts.AI;
using Managers;
using Model;
using States.Abstraction;
using UnityEngine;

namespace States
{
    public class PlayerOTurnState : GameState
    {

        public override void EnterState(GameManager gameManager)
        {

            gameManager.CurrentMove = PMove.O;
            gameManager.gameStateManager.CurrentGameState = this;
            gameManager.UpdateStatus();

            if (gameManager.playerOType == PlayerType.AI)
            {
                // TODO
            }
        }

        public override void UpdateState(GameManager gameManager)
        {
            if (gameManager.BoardCheckWin() != PMove.None)
                gameManager.gameResult = GameResult.O;

            if (gameManager.gameResult != GameResult.InProgress || gameManager.moveCount >= 9)
            {
                gameManager.gameStateManager.SetState(new GameOverState(), gameManager);
            }
            
            gameManager.gameStateManager.SetState(new PlayerXTurnState(), gameManager);
        }

        public override void ExitState(GameManager gameManager) { }
    }
}
