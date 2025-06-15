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
           
            gameManager.GameStateManager.CurrentGameState = this;

            if (gameManager.playerXType == PlayerType.AI)
            {
                // TODO
            }
        }

        public override void UpdateState(GameManager gameManager)
        {
            if (gameManager.board.GetModel().CheckWin() != PMove.None || gameManager.moveCount >= 9)
            {
                gameManager.GameStateManager.SetState(gameManager.GameStateManager.GameOverState, gameManager);
                Debug.Log("Game Over State");
            }
            
            gameManager.GameStateManager.SetState(gameManager.GameStateManager.OTurnState, gameManager);
            
        }

        public override void ExitState(GameManager gameManager)
        {
            // Clean up or reset any necessary variables or states here
        }
    }
}
