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
            Debug.Log("Entered Player X Turn State");
            gameManager.CurrentMove = PMove.X;
           
            gameManager.GameStateManager.CurrentGameState = this;

            if (gameManager.playerXType == PlayerType.AI)
            {
                // TODO
            }
        }

        public override void UpdateState(GameManager gameManager)
        {
            Debug.Log("Updating Player X Turn State");
            if (gameManager.selectedCell != null)
            {
                Debug.Log($"Selected cell: {gameManager.selectedCell}");
                gameManager.moveCount++;
                gameManager.selectedCell = null;
                
                gameManager.GameStateManager.SetState(gameManager.GameStateManager.OTurnState, gameManager);
            }
        }

        public override void ExitState(GameManager gameManager)
        {
            // Clean up or reset any necessary variables or states here
        }
    }
}
