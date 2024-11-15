using CodeBase.Core.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Implementation.Modules.Base.MainMenu
{
    public class MainMenuScreenInstaller : MonoInstaller<MainMenuScreenInstaller>
    {
        [Inject] private RootCanvas _rootCanvas;
        [SerializeField] private MainMenuScreenView mainMenuScreenView;
        
        public override void InstallBindings()
        {
            Container
                .Bind<MainMenuScreenModel>()
                .AsTransient();
            
            Container
                .Bind<MainMenuScreenViewModel>()
                .AsTransient();
            
            Container
                .Bind<MainMenuScreenView>()
                .FromComponentInNewPrefab(mainMenuScreenView)
                .UnderTransform(_rootCanvas.SaveZoneParent)
                .AsTransient();
        }
    }
}