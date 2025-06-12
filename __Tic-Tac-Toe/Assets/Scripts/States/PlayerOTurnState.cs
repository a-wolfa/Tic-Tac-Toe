using Assets.Scripts.AI;
using Managers;
using Model;
using States.Abstraction;

namespace States
{
    public class PlayerOTurnState : IGameState
    {
        public void EnterState(GameManager gameManager)
        {
            gameManager.CurrentMove = PMove.O;

            if (gameManager.playerOType == PlayerType.AI)
            {
                // TODO
            }
        }

        public void UpdateState(GameManager gameManager)
        {
            // TODO
        }

        public void ExitState(GameManager gameManager) { }
    }
}
