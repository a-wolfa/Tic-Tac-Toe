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
            Debug.Log("Game Over State Entered");
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
