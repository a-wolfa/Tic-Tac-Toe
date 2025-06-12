using Line;
using Managers;
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

            Container.Bind<GameStateManager>().AsSingle();
        }

        public void AddStates()
        {
        }
    }
}
