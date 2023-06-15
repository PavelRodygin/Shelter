using Core.Views;
using GameScripts;
using UnityEngine;
using Zenject;

namespace UIModules.GameScreen.Scripts
{
    public class GameScreenInstaller: MonoInstaller
    {
        [SerializeField] private GameScreenUIView gameScreenUIViewPrefab;
        [SerializeField] private GameplayModule gameplayModule;
        
        public override void InstallBindings()
        { 
            Container.Bind<GameScreenUIView>().FromComponentInNewPrefab(gameScreenUIViewPrefab)
                .UnderTransform(c => c.Container.Resolve<RootCanvas>().transform).AsTransient();
            Container.Bind<GameScreenController>().AsTransient();
            Container.Bind<GameplayModule>().FromInstance(gameplayModule).AsSingle();
        }
    }
}