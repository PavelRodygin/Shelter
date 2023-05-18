using Core.Views;
using UnityEngine;
using Zenject;

namespace UIModules.GameScreen.Scripts
{
    public class GameScreenInstaller: MonoInstaller
    {
        [SerializeField] private GameScreenUIView gameScreenUIViewPrefab;
        
        public override void InstallBindings()
        { 
            Container.Bind<GameScreenUIView>().FromComponentInNewPrefab(gameScreenUIViewPrefab)
                .UnderTransform(c => c.Container.Resolve<RootCanvas>().transform).AsTransient();
            Container.Bind<GameScreenController>().AsTransient();
        }
    }
}