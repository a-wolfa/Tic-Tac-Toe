using Core;
using Managers;
using States.Abstraction;

namespace States
{
    public class OTurnState : GameState
    {
        public override GameStateManager manager { get; set; }

        public override void EnterState(GameManager gameManager)
        {
            gameManager.SetActivePlayer(PlayerType.O);
        }

        public override void ExitState(GameManager gameManager)
        {
        }
    }
}
