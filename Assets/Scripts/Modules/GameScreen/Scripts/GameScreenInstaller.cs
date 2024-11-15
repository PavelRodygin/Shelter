using Core.Views;
using UnityEngine;
using Zenject;

namespace Modules.GameScreen.Scripts
{
    public class GameScreenInstaller: MonoInstaller<GameScreenInstaller>
    {   
        [SerializeField] private GameScreenView gameScreenViewPrefab;
        [SerializeField] private LevelManager levelManager;
        
        public override void InstallBindings()
        {
            Container.Bind<GameScreenController>().AsTransient();
            
            Container
                .Bind<GameScreenView>()
                .FromComponentInNewPrefab(gameScreenViewPrefab)
                .UnderTransform(c => c.Container.Resolve<RootCanvas>().transform)
                .AsTransient();
            
            Container
                .Bind<LevelManager>()
                .FromInstance(levelManager)
                .AsSingle();
            
            Container
                .Bind<GameMessageManager>()
                .AsTransient();
        }
    }
}