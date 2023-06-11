using Core.Views;
using GameScripts.Level;
using UnityEngine;
using Zenject;

namespace UIModules.GameScreen.Scripts
{
    public class LevelInstaller: MonoInstaller
    {
        [SerializeField] private GameScreenUIView gameScreenUIViewPrefab;
        [SerializeField] private GameplayModule gameplayModulePrefab;
        
        public override void InstallBindings()
        { 
            Container.Bind<GameScreenUIView>().FromComponentInNewPrefab(gameScreenUIViewPrefab)
                .UnderTransform(c => c.Container.Resolve<RootCanvas>().transform).AsTransient();
            Container.Bind<LevelController>().AsTransient();
            Container.Bind<GameplayModule>().FromInstance(gameplayModulePrefab).AsSingle();
        }
    }
}