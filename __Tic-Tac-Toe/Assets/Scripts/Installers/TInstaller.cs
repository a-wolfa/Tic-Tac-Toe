using Managers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class TInstaller : MonoInstaller
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private UIManager uiManager;
        public override void InstallBindings()
        {
            Container.Bind<GameManager>().FromInstance(gameManager).AsSingle().NonLazy();
            Container.Bind<UIManager>().FromInstance(uiManager).AsSingle().NonLazy();
            
            // Bind other dependencies here
        }
    }
}
