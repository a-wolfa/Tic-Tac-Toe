using Managers;
using Model;
using States.Abstraction;
using UnityEngine;

namespace States
{
    public class GameOverState : GameState
    {
        public override void EnterState(GameManager gameManager)
        {
            if (gameManager.gameResult == GameResult.InProgress)
                gameManager.gameResult = GameResult.Draw;

            gameManager.UpdateStatus();
        }

        public override void UpdateState(GameManager gameManager)
        {
            // No further update here
        }

        public override void ExitState(GameManager gameManager)
        {
            // Clean up or reset any necessary variables or states here
        }
    }
}
