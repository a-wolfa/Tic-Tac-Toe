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
            Debug.Log("Entered Game Over State");
            gameManager.gameResult = GetGameResult(gameManager);
            gameManager.CurrentMove = PMove.None;
            
            gameManager.NotifyGameOver(gameManager.gameResult);
        }

        public override void UpdateState(GameManager gameManager)
        {
            // No further update here
        }

        public override void ExitState(GameManager gameManager)
        {
            // Clean up or reset any necessary variables or states here
        }

        public GameResult GetGameResult(GameManager gameManager)
        {
            // This is a placeholder; replace with your actual game logic
            if (gameManager.CurrentMove == PMove.X)
            {
                return GameResult.X;
            }
            else if (gameManager.CurrentMove == PMove.O)
            {
                return GameResult.O;
            }
            else
            {
                return GameResult.Draw;
            }
        }
    }
}
