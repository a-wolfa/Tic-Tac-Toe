using Core;
using Managers;
using States.Abstraction;

namespace States
{
    public class XTurnState : GameState
    {
        public override GameStateManager manager { get; set; }

        public override void EnterState(GameManager gameManager)
        {
            gameManager.SetActivePlayer(PlayerType.X);
        }
        
        public override void ExitState(GameManager gameManager)
        {
        }
    }
}
