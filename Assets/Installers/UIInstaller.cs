using DefaultNamespace.UI;
using UI;
using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameScreen gameScreen;
    [SerializeField] private MainScreen mainScreen;
    [SerializeField] private SettingsScreen settingsScreen;
    public override void InstallBindings()
    {
        Container.BindInstance(uiManager).AsSingle();
        Container.BindInstance(gameScreen).AsSingle();
        Container.BindInstance(settingsScreen).AsSingle();
        Container.BindInstance(mainScreen).AsSingle();
    }
}