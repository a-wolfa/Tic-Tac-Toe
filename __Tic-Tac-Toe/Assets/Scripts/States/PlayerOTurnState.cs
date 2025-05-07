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
            gameManager.CurrentPlayer = Player.O;
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
