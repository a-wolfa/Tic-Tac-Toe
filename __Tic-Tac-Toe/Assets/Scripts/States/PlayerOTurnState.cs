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
            if (gameManager.selectedCell != null)
            {
                Debug.Log($"Selected cell: {gameManager.selectedCell}");
                gameManager.moveCount++;
                gameManager.selectedCell = null;
                
                gameManager.GameStateManager.SetState(gameManager.GameStateManager.XTurnState, gameManager);
            }
        }

        public override void ExitState(GameManager gameManager) { }
    }
}
