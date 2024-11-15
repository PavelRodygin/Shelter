using System;
using CodeBase.Core.Infrastructure;
using CodeBase.Core.MVVM;
using CodeBase.Core.Systems;
using Cysharp.Threading.Tasks;

namespace CodeBase.Implementation.Modules.Base.StartScreen.Scripts
{
    public class StartGameScreenViewModel : IScreenViewModel
    {
        private readonly IScreenStateMachine _screenStateMachine;
        private readonly StartGameScreenModel _screenModel;
        private readonly StartGameScreenView _screenView;
        private readonly PopupHub _popupHub;
        private readonly UniTaskCompletionSource<bool> _completionSource;
        
        private static string _appVersion;
        
        public bool CanExit { get; private set; } = true;
        private float exponentialProgress { get; set; }
        private string progressStatus { get; set; }

        public StartGameScreenViewModel(IScreenStateMachine screenStateMachine, StartGameScreenView screenView, StartGameScreenModel screenModel, PopupHub popupHub)
        {
            _screenStateMachine = screenStateMachine;
            _screenView = screenView;
            _screenModel = screenModel;
            _popupHub = popupHub;
            _completionSource = new UniTaskCompletionSource<bool>();

            _screenModel.OnProgressUpdated += UpdateViewWithModelData;
            // _screenModel.OnInitializationCompleted += ShowContinueButton;
        }

        public async UniTask Enter(object param)
        {
            _screenView.SetupEventListeners(OnContinueButtonPressed);
            
            await _screenView.Show();
            
            _screenView.ResetProgressIndicators();
            await StartLoad();
            
            ShowContinueButton();
            
            await _completionSource.Task;
        }

        public void ShowContinueButton() => _screenView.ShowContinueButton();
        
        public void NativeExit()
        {
            CanExit = false;
            // _popupHub.CloseCurrentPopup().Forget();
        }
        
        public async UniTask Exit() 
        {
            await _screenView.Hide();
        }
        

        public void Dispose()
        {
            _screenView.Dispose();
            
            _screenModel.OnProgressUpdated -= UpdateViewWithModelData;
            // _screenModel.OnInitializationCompleted -= ShowContinueButton;
            _screenModel.Dispose();
        }

        private async UniTask StartLoad()
        {
            await _screenModel.InitializeAppServices();
        }
        
        private void UpdateViewWithModelData(float progress/*, string serviceName*/)
        {
            UpdateProgress(progress);
            _screenView.ReportProgress(exponentialProgress, "No loadingStatus").Forget();
        }

        private void UpdateProgress(float progress) => 
            exponentialProgress = CalculateExponentialProgress(progress);

        private float CalculateExponentialProgress(float progress)
        {
            var expValue = Math.Exp(progress);
            var minExp = Math.Exp(0);
            var maxExp = Math.Exp(1);
            return (float)((expValue - minExp) / (maxExp - minExp));
        }
        
        private void OnContinueButtonPressed()
        {
            
        }

        private void RunNewScreen(ScreenViewModelMap screen)
        {
            _completionSource.TrySetResult(true);
            _screenStateMachine.RunScreen(screen);
        }
    }
}