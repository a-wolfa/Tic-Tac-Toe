using Managers;

namespace States.Abstraction
{
    public abstract class GameState
    {
        public virtual GameStateManager manager { get; set; }
        public abstract void EnterState(GameManager gameManager);
        public abstract void ExitState(GameManager gameManager);
    }
}
