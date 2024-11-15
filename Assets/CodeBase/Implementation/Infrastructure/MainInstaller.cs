using CodeBase.Core.Infrastructure;
using CodeBase.Core.Systems;
using CodeBase.Core.Systems.DataPersistenceSystem;
using CodeBase.Core.UI;
using CodeBase.Implementation.FavoriteAddonSystem;
using CodeBase.Implementation.Popup.PopupFactories;
using CodeBase.Implementation.Popup.Popups;
using CodeBase.Services;
using CodeBase.Services.App;
using UnityEngine;
using Zenject;

namespace CodeBase.Implementation.Infrastructure
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private ScreenStateMachine screenStateMachine;
        [SerializeField] private RootCanvas rootCanvas;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private PopupHub popupHub;
        [SerializeField] private AudioSystem audioSystem;
        [SerializeField] private ApplicationService applicationService;
        [SerializeField] private NativeInputSystem nativeInputSystem;
        [SerializeField] private DataPersistenceManager dataPersistenceManager;
        
        [Header("Popup prefabs")] 
        [SerializeField] private LoadingQuitDialogPopup loadingQuitDialogPopup;
        [SerializeField] private AdsDialogBoxPopup adsDialogBoxPopup;
        [SerializeField] private AddonVersionsPopup addonVersionsPopup;
        [SerializeField] private LoadingErrorPopup loadingErrorPopup;
        
        // [Header("Pools prefabs")]
        // [SerializeField] private AddonScrollRectCardView addonCardView;

        public override void InstallBindings()
        {
            Container
                .Bind<IScreenStateMachine>()
                .To<ScreenStateMachine>()
                .FromInstance(screenStateMachine)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<ScreenTypeMapper>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<RootCanvas>()
                .FromInstance(rootCanvas)
                .AsSingle();
            
            Container
                .Bind<Camera>()
                .FromInstance(mainCamera)
                .AsSingle();

            BindSystems();
            BindServices();
            BindPopupHubFactories();
            BindAddonScrollRectCards();
        }

        private void BindSystems()
        {
            Container.Bind<PopupHub>()
                .FromInstance(popupHub)
                .AsSingle();
            
            Container
                .Bind<FavoriteAddonsSystem>()   
                .AsSingle();
            
            Container.Bind<AudioSystem>()
                .FromInstance(audioSystem)
                .AsSingle();
            
            Container.Bind<NativeInputSystem>()
                .FromInstance(nativeInputSystem)
                .AsSingle();
           
            Container.Bind<DataPersistenceManager>()
                .FromInstance(dataPersistenceManager)
                .AsSingle();
        }
        
        private void BindServices()
        {
            Container.Bind<IApplicationService>().FromInstance(applicationService).AsSingle().NonLazy();

            Container.Bind<TestLongInitializationService>().AsSingle();
        }

        private void BindPopupHubFactories()
        {   
            Container
                .Bind<AdsDialogBoxPopupFactory>()
                .AsTransient()
                .WithArguments(adsDialogBoxPopup)
                .NonLazy();     
            
            Container
                .Bind<LoadingQuitDialogPopupFactory>()
                .AsTransient()
                .WithArguments(loadingQuitDialogPopup)
                .NonLazy();
            
            Container
                .Bind<AddonVersionsPopupFactory>()
                .AsTransient()
                .WithArguments(addonVersionsPopup)
                .NonLazy();    
            Container
                .Bind<LoadingErrorPopupFactory>()
                .AsTransient()
                .WithArguments(loadingErrorPopup)
                .NonLazy();
        }

        private void BindAddonScrollRectCards()
        {
            // Container
            //     .BindMemoryPool<AddonScrollRectCardView, AddonsScrollRectCardViewPool>()
            //     .FromComponentInNewPrefab(addonCardView)
            //     .UnderTransform(rootCanvas.SaveZoneParent);
            //
            // Container
            //     .Bind<AddonsScrollRectCardViewModel>()
            //     .FromNew()
            //     .AsTransient();
            // Container
            //     .Bind<AddonsScrollRectCardModel>()
            //     .FromNew()
            //     .AsTransient();
        }
    }
}