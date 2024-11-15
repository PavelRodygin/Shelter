using UnityEngine;
using Zenject;

namespace CodeBase.Implementation.Modules.Base.StartScreen.Scripts
{
    public class StartGameScreenInstaller : MonoInstaller<StartGameScreenInstaller>
    {
        [SerializeField] private StartGameScreenView startGameScreenView;

        public override void InstallBindings()
        {
            Container
                .Bind<StartGameScreenModel>()
                .AsTransient();
            
            Container
                .Bind<StartGameScreenViewModel>()
                .AsTransient();
            
            Container
                .Bind<StartGameScreenView>()
                .FromInstance(startGameScreenView)
                .AsSingle();
        }
    }
}