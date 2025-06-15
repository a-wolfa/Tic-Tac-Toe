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
            
            gameManager.GameStateManager.CurrentGameState = this;

            if (gameManager.playerOType == PlayerType.AI)
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
            Debug.Log("Switching to X Turn State");
            
            gameManager.GameStateManager.SetState(gameManager.GameStateManager.XTurnState, gameManager);
        }

        public override void ExitState(GameManager gameManager) { }
    }
}
