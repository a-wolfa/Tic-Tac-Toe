using Managers;
using Model;
using States.Abstraction;
using UnityEngine;

namespace States
{
    public class PlayerXTurnState : IGameState
    {
        public void EnterState(GameManager gameManager)
        {
            gameManager.CurrentPlayer = Player.X;
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
