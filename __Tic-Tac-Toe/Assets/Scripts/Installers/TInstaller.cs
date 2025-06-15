using Board.Controllers;
using Line;
using Managers;
using States;
using States.Abstraction;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class TInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            AddManagers();
            AddStates();
        }

        public void AddManagers()
        {
            Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<LineRenderer>().FromComponentInHierarchy().AsSingle();
            Container.Bind<BoardController>().FromComponentInHierarchy().AsSingle();

            Container.Bind<GameStateManager>().AsSingle();
        }

        public void AddStates()
        {
            Container.Bind<GameState>().WithId("PlayerX").To<PlayerXTurnState>().AsSingle();
            Container.Bind<GameState>().WithId("PlayerO").To<PlayerOTurnState>().AsSingle();
            Container.Bind<GameState>().WithId("GameOver").To<GameOverState>().AsSingle();
        }
    }
}
