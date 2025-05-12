using Managers;
using Model;
using States.Abstraction;
using UnityEngine;

namespace States
{
    public class GameOverState : IGameState
    {
        public void EnterState(GameManager gameManager)
        {
            Debug.Log("Game is Actually Over");
            if (gameManager.CheckForWinner())
                gameManager.WinnerFound.Invoke();
        }

        public void UpdateState(GameManager gameManager)
        {
            // No further update here
        }

        public void ExitState(GameManager gameManager)
        {
            // Clean up or reset any necessary variables or states here
        }
    }
}
