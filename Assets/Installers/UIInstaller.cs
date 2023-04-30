using UIModules;
using UIModules.GameScreen.Scripts;
using UIModules.MainMenu.Scripts;
using UIModules.SettingsScreen.Scripts;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class UIInstaller : MonoInstaller
    {
        //[SerializeField] private UIManager uiManager;
        [SerializeField] private GameScreen gameScreen;
        [SerializeField] private MainMenu mainMenu;
        [SerializeField] private SettingsScreen settingsScreen;
        public override void InstallBindings()
        {
            //Container.BindInstance(uiManager).AsSingle();
            Container.BindInstance(gameScreen).AsSingle();
            Container.BindInstance(settingsScreen).AsSingle();
            Container.BindInstance(mainMenu).AsSingle();
        }
    }
}