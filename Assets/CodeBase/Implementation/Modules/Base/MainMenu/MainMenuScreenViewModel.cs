using CodeBase.Core.Infrastructure;
using CodeBase.Core.MVVM;
using Cysharp.Threading.Tasks;

namespace CodeBase.Implementation.Modules.Base.MainMenu
{
    public class MainMenuScreenViewModel : IScreenViewModel
    {
        private readonly IScreenStateMachine _screenStateMachine;
        private readonly MainMenuScreenModel _screenModel;
        private readonly MainMenuScreenView _screenView;
        private readonly UniTaskCompletionSource<bool> _completionSource;

        public bool CanExit { get; private set; } = true;

        public MainMenuScreenViewModel(IScreenStateMachine screenStateMachine, 
            MainMenuScreenModel screenModel, MainMenuScreenView screenView)
        {
            _screenStateMachine = screenStateMachine;
            _screenModel = screenModel;
            _screenView = screenView;
            _completionSource = new UniTaskCompletionSource<bool>();
        }
        
        public async UniTask Enter(object param)
        {
            _screenView.HideInstantly();
            _screenView.InitializeToggle(_screenModel.GetSoundValue());
            _screenView.SetupEventListeners
            (
                OnAddonListButtonClicked,
                OnFavoritesButtonClicked,
                OnInstructionButtonClicked,
                OnSettingsButtonClicked,
                OnSoundToggleClicked,
                OnPrivacyPolicyButtonClicked
            );
            await _screenView.Show();
            await _completionSource.Task;
        }

        public async UniTask Exit() => await _screenView.Hide();
        
        public void NativeExit()
        {
            CanExit = false;
            //TODO No possible exits
        }
        
        public void Dispose()
        {
            _screenModel.Dispose();
            _screenView.Dispose();
        }

        private void OnSoundToggleClicked(bool isToggled) => _screenModel.SetSoundVolume(!isToggled);
        
        private void OnAddonListButtonClicked()
        {
#if LOCAL_ENABLED
            RunNewScreen(ScreenViewModelMap.AddonList);
#elif SERVER_ENABLED
            RunNewScreen(ScreenViewModelMap.CategoryAddonList);
#endif
        }

        private void OnFavoritesButtonClicked() => RunNewScreen(ScreenViewModelMap.FavoriteAddonList);

        private void OnInstructionButtonClicked() => RunNewScreen(ScreenViewModelMap.Instructions);

        private void OnSettingsButtonClicked() => RunNewScreen(ScreenViewModelMap.Settings);

        private void OnPrivacyPolicyButtonClicked() => 
            RunNewScreen(ScreenViewModelMap.PrivacyPolicy, ScreenViewModelMap.MainMenu);
        
        private void RunNewScreen(ScreenViewModelMap screen, object param = null)
        {
            _completionSource.TrySetResult(true);
            _screenStateMachine.RunScreen(screen, param);
        }
    }
}