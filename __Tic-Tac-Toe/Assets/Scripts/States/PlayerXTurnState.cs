using System.ComponentModel;
using Assets.Scripts.AI;
using Managers;
using Model;
using States.Abstraction;
using UnityEngine;
using Zenject;

namespace States
{
    public class PlayerXTurnState : GameState
    {

        public override void EnterState(GameManager gameManager)
        {
            gameManager.CurrentMove = PMove.X;
            gameManager.gameStateManager.CurrentGameState = this;
            gameManager.UpdateStatus();

            if (gameManager.playerXType == PlayerType.AI)
            {
                // TODO
            }
        }

        public override void UpdateState(GameManager gameManager)
        {

            if (gameManager.CheckWin() != PMove.None)
                gameManager.gameResult = GameResult.X;

            if (gameManager.gameResult != GameResult.InProgress || gameManager.moveCount >= 9)
            {
                gameManager.gameStateManager.SetState(new GameOverState(), gameManager);
            }

            gameManager.gameStateManager.SetState(new PlayerOTurnState(), gameManager);
        }

        public override void ExitState(GameManager gameManager)
        {
            // Clean up or reset any necessary variables or states here
        }
    }
}
