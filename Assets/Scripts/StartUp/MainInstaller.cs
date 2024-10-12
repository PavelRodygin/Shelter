using Core.Controllers;
using Core.Systems;
using Core.Systems.DataPersistenceSystem;
using Core.Views;
using Services;
using UnityEngine;
using Zenject;

namespace StartUp
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private RootController rootController;
        [SerializeField] private RootCanvas rootCanvas;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private AudioSystem audioSystem;
        [SerializeField] private DataPersistenceManager dataPersistenceManager;
        
        public override void InstallBindings()
        {
            Container.Bind<ScreenTypeMapper>().AsSingle().NonLazy();
            Container.Bind<IRootController>().To<RootController>().FromInstance(rootController).AsSingle().NonLazy();
            
            BindServices();
            Container.Bind<RootCanvas>().FromInstance(rootCanvas).AsSingle();
            Container.Bind<Camera>().FromInstance(mainCamera).AsSingle();
        }

        private void BindServices()
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                Container.Bind<IInputService>().To<MobileInputService>().AsSingle();
            else if (Application.isEditor) 
                Container.Bind<IInputService>().To<StandaloneInputService>().AsSingle();
            
            Container.Bind<AudioSystem>().FromInstance(audioSystem).AsSingle();
            Container.Bind<DataPersistenceManager>().FromInstance(dataPersistenceManager).AsSingle();
        }
    }
}