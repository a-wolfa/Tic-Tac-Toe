using Line;
using Managers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class TInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<LineRenderer>().FromComponentInHierarchy().AsSingle();
            // Bind other dependencies here
        }
    }
}
