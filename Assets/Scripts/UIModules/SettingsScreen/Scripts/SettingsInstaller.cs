using Core.Views;
using UnityEngine;
using Zenject;

namespace UIModules.SettingsScreen.Scripts
{
    public class SettingsInstaller : MonoInstaller<SettingsInstaller>
    {
        [SerializeField] private SettingsUIView settingsUIViewPrefab;
        
        public override void InstallBindings()
        { 
            Container.Bind<SettingsUIView>().FromComponentInNewPrefab(settingsUIViewPrefab)
                .UnderTransform(c => c.Container.Resolve<RootCanvas>().transform).AsTransient();
            Container.Bind<SettingsController>().AsTransient();
        }
    }
}