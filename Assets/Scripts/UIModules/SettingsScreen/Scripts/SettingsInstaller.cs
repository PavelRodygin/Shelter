using Core.Views;
using Modules.Settings.Scripts;
using UIModules.MainMenu.Scripts;
using UnityEngine;
using Zenject;

namespace UIModules.SettingsScreen.Scripts
{
    public class SettingsInstaller : MonoInstaller
    {
        [SerializeField] private SettingsUIView settingsUIViewPrefab;
        
        public override void InstallBindings()
        { 
            Container.Bind<MainMenuUIView>().FromComponentInNewPrefab(settingsUIViewPrefab)
                .UnderTransform(c => c.Container.Resolve<RootCanvas>().transform).AsTransient();
            Container.Bind<SettingsController>().AsTransient();
        }
    }
}