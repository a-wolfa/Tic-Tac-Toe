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
            
            gameManager.CurrentMove = PMove.X;

            if (gameManager.playerXType == PlayerType.AI)
            {
                // TODO
            }
        }

        public void UpdateState(GameManager gameManager)
        {
            // TODO
        }

        public void ExitState(GameManager gameManager)
        {
            // Clean up or reset any necessary variables or states here
        }
    }
}
