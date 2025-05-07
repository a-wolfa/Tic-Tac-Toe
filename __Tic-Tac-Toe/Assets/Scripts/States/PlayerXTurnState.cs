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
            Debug.Log("Player X's Turn");
            gameManager.CurrentPlayer = Player.X;
        }

        public void UpdateState(GameManager gameManager)
        {
            Debug.Log(gameManager.CheckForWinner());
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
