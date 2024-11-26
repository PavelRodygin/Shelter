using CodeBase.Core.Infrastructure;
using CodeBase.Core.MVVM;
using Cysharp.Threading.Tasks;

namespace CodeBase.Implementation.Modules.Template.TemplateScreen
{
    public class TemplateScreenViewModel : IScreenViewModel
    {
        private readonly IScreenStateMachine _screenStateMachine;
        private readonly TemplateScreenModel _templateScreenViewModel;
        private readonly TemplateScreenView _templateScreenView;
        private readonly UniTaskCompletionSource<bool> _completionSource;
        
        public bool CanExit { get; private set; } = true;

        public TemplateScreenViewModel(IScreenStateMachine screenStateMachine, 
            TemplateScreenModel templateScreenViewModel, TemplateScreenView templateScreenView)
        {
            _screenStateMachine = screenStateMachine;
            _templateScreenViewModel = templateScreenViewModel;
            _templateScreenView = templateScreenView;
            _completionSource = new UniTaskCompletionSource<bool>();
        }
        
        public async UniTask Enter(object param)
        {
            _templateScreenView.HideInstantly();
            _templateScreenView.SetupEventListeners
            (
                OnBackButtonClicked
            );
            await _templateScreenView.Show();
            await _completionSource.Task;
        }

        public async UniTask Exit() => await _templateScreenView.Hide();
        public void NativeExit()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            //If model is used, there must be removing event listeners of model
            _templateScreenView.Dispose();
            _templateScreenViewModel.Dispose();
        }
        
        private void OnBackButtonClicked()
        {
            RunNewScreen(ScreenViewModelMap.Bootstrap);
        }

        private void RunNewScreen(ScreenViewModelMap screen, object param = null)
        {
            _completionSource.TrySetResult(true);
            _screenStateMachine.RunScreen(screen, param);
        }
    }
}
