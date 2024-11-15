using System;
using System.Threading;
using CodeBase.Core.Infrastructure;
using CodeBase.Core.MVVM;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Implementation.Modules.Base.PrivacyPolicy
{
    public class PrivacyPolicyScreenViewModel : IScreenViewModel
    {
        private readonly IScreenStateMachine _screenStateMachine;
        private readonly PrivacyPolicyScreenView _privacyPolicyScreenView;
        private readonly UniTaskCompletionSource<bool> _completionSource;
        private ScreenViewModelMap _previousScreen;

        public bool CanExit { get; private set; } = true;

        public PrivacyPolicyScreenViewModel(IScreenStateMachine screenStateMachine, 
            PrivacyPolicyScreenView privacyPolicyScreenView)
        {
            _screenStateMachine = screenStateMachine;
            _privacyPolicyScreenView = privacyPolicyScreenView;
            _completionSource = new UniTaskCompletionSource<bool>();
        }
        
        public async UniTask Enter(object param)
        {
            if (param is ScreenViewModelMap previousScreen)
                _previousScreen = previousScreen;
            else
                Debug.LogError("Param is not PreviousScreen");
            
            _privacyPolicyScreenView.HideInstantly();
            _privacyPolicyScreenView.SetupEventListeners(OnBackButtonClicked);
            await _privacyPolicyScreenView.Show();
            await _completionSource.Task;
        }

        public void NativeExit()
        {
            CanExit = false;
            OnBackButtonClicked();
        } 
        
        public async UniTask Exit() => await _privacyPolicyScreenView.Hide();

        public void Dispose() => _privacyPolicyScreenView.Dispose();

        private void OnBackButtonClicked()
        {
            CanExit = false;
            RunNewScreen(_previousScreen);
        }

        private void RunNewScreen(ScreenViewModelMap screen)
        {
            _completionSource.TrySetResult(true);
            _screenStateMachine.RunScreen(screen);
        }
    }
}