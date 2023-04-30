using Core.Controllers;
using Core.Views;
using Systems.AudioSystem;
using UnityEngine;
using Zenject;

namespace Start
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private RootController rootController;
        [SerializeField] private RootCanvas rootCanvas;
        [SerializeField] private AudioSystem audioSystem;
        [SerializeField] private Camera mainCamera;
        //[SerializeField] private DataPersistenceManager dataPersistenceManager;
        //[SerializeField] private ADManager adManager;
        //[SerializeField] private PurchasesManager purchasesManager;

        public override void InstallBindings()
        {
            //Container.Bind<DataPersistenceManager>().FromInstance(dataPersistenceManager).AsSingle();
            Container.Bind<AudioSystem>().FromInstance(audioSystem).AsSingle();
            //Container.Bind<ADManager>().FromInstance(adManager);
            //Container.Bind<PurchasesManager>().FromInstance(purchasesManager);
            Container.Bind<ControllerMapper>().AsSingle().NonLazy();
            Container.Bind<IRootController>().To<RootController>().FromInstance(rootController).AsSingle().NonLazy();
            Container.Bind<RootCanvas>().FromInstance(rootCanvas).AsSingle();
            Container.Bind<Camera>().FromInstance(mainCamera).AsSingle();
        }
    }
}