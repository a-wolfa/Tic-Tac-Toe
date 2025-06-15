using Managers;
using Zenject;

namespace States.Abstraction
{
    public abstract class GameState
    {
        public abstract void EnterState(GameManager gameManager);
        public abstract void UpdateState(GameManager gameManager);
        public abstract void ExitState(GameManager gameManager);
    }
}
